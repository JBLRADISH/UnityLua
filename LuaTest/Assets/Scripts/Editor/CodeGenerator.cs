using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Reflection;
using System;
using System.Linq;

public static class CodeGenerator
{
    [MenuItem("Tools/WrapGenerator")]
    public static void WrapGenerator()
    {
        string directoryPath = Application.dataPath + "/Scripts/Wrap";
        if (Directory.Exists(directoryPath))
        {
            Directory.Delete(directoryPath, true);
        }
        Directory.CreateDirectory(directoryPath);
        for (int i = 0; i < WrapSettings.wrapClasses.Count; i++)
        {
            WrapClassGenerator(WrapSettings.wrapClasses[i]);
        }
        AssetDatabase.Refresh();
    }

    private static void WrapClassGenerator(Type t)
    {
        if (t != typeof(UnityEngine.Object))
        {
            WrapClassGenerator(t.BaseType);
        }
        string filePath = Application.dataPath + "/Scripts/Wrap/" + t.Name + "Wrap.cs";
        FileStream fs = new FileStream(filePath, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("public static class " + t.Name + "Wrap");
        sw.WriteLine("{");
        WrapRegisterGenerator(t, sw);
        sw.WriteLine("}");
        sw.Close();
        fs.Close();
    }

    private static void WrapRegisterGenerator(Type t, StreamWriter sw)
    {
        sw.WriteLine("public static void Register()");
        sw.WriteLine("{");
        sw.WriteLine(string.Format("LuaCallback.BeginClass(typeof({0}), {1});", t.FullName, t.BaseType == typeof(object) ? "null" : "typeof(" + t.BaseType.FullName + ")"));
        Dictionary<string, List<MethodInfo>> funcInfos = new Dictionary<string, List<MethodInfo>>();
        MethodInfo[] methodInfos = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        for (int i = 0; i < methodInfos.Length; i++)
        {
            MethodInfo methodInfo = methodInfos[i];
            if (!methodInfo.IsSpecialName && !methodInfo.IsGenericMethod && !CheckObsolete(methodInfo))
            {
                if (!funcInfos.ContainsKey(methodInfo.Name))
                {
                    funcInfos[methodInfo.Name] = new List<MethodInfo>();
                }
                funcInfos[methodInfo.Name].Add(methodInfo);
            }
        }
        foreach (string item in funcInfos.Keys)
        {
            sw.WriteLine(string.Format("LuaCallback.RegisterFunc(\"{0}\", {0});", item));
        }
        //FieldInfo[] fieldInfos = t.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        //for (int i = 0; i < fieldInfos.Length; i++)
        //{
        //    FieldInfo fieldInfo = fieldInfos[i];
        //    if (!CheckObsolete(fieldInfo))
        //    {
        //        sw.WriteLine(string.Format("LuaCallback.RegisterVar(\"{0}\", get_{0}, {1});", fieldInfo.Name, fieldInfo.IsLiteral ? "null" : "set_" + fieldInfo.Name));
        //    }
        //}
        //PropertyInfo[] propertyInfos = t.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        //for (int i = 0; i < propertyInfos.Length; i++)
        //{
        //    PropertyInfo propertyInfo = propertyInfos[i];
        //    if (!CheckObsolete(propertyInfo))
        //    {
        //        sw.WriteLine(string.Format("LuaCallback.RegisterVar(\"{0}\", {1}, {2});", propertyInfo.Name, propertyInfo.CanRead ? "get_" + propertyInfo.Name : "null", propertyInfo.CanWrite ? "set_" + propertyInfo.Name : "null"));
        //    }
        //}
        sw.WriteLine("LuaCallback.EndClass();");
        sw.WriteLine("}");

        foreach (var kv in funcInfos)
        {
            WrapFuncGenerator(sw, kv.Value);
        }
    }

    private static void WrapFuncGenerator(StreamWriter sw, List<MethodInfo> funcInfos)
    {
        sw.WriteLine("");
        sw.WriteLine("[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]");
        sw.WriteLine(string.Format("public static int {0}(System.IntPtr L)", funcInfos[0].Name));
        sw.WriteLine("{");
        sw.WriteLine("int nargs = LuaAPI.GetTop(L);");
        for (int i = 0; i < funcInfos.Count; i++)
        {
            MethodInfo funcInfo = funcInfos[i];
            ParameterInfo[] parameterInfos = funcInfo.GetParameters();
            string condition = string.Format("if (nargs == {0}", funcInfo.IsStatic ? parameterInfos.Length : parameterInfos.Length + 1);
            condition += funcInfo.IsStatic ? "" : TypeChecker(funcInfo.DeclaringType, 1);
            for (int j = 0; j < parameterInfos.Length; j++)
            {
                condition += TypeChecker(parameterInfos[j].ParameterType, j + (funcInfo.IsStatic ? 1 : 2));
            }
            condition += ")";
            sw.WriteLine(condition);
            sw.WriteLine("{");
            sw.WriteLine(funcInfo.IsStatic ? null : TypeConverter(funcInfo.DeclaringType, 0, 1));
            for (int j = 0; j < parameterInfos.Length; j++)
            {
                sw.WriteLine(TypeConverter(parameterInfos[j].ParameterType, j + (funcInfo.IsStatic ? 0 : 1), j + (funcInfo.IsStatic ? 1 : 2)));
            }
            sw.WriteLine(CallConverter(funcInfo));
            sw.WriteLine(PushConverter(funcInfo));
            sw.WriteLine(string.Format("return {0};", GetNResults(funcInfo)));
            sw.WriteLine("}");
        }
        sw.WriteLine("return 0;");
        sw.WriteLine("}");
    }

    private static bool ContainsKey(Dictionary<Type, string> dict, Type t, out string s)
    {
        Type tmp = t;
        while (t != null)
        {
            if (dict.ContainsKey(t))
            {
                s = dict[t];
                return true;
            }
            t = t.BaseType;
        }
        s = null;
        return false;
    }

    private static Dictionary<Type, string> typeCheckerDict = new Dictionary<Type, string>()
    {
        { typeof(int), "Number"},
        { typeof(float), "Number"},
        { typeof(Enum), "Number"},
        { typeof(Type), "Number"},
        { typeof(bool), "Bool"},
        { typeof(string), "String"},
        { typeof(Vector2), "Vector2"},
        { typeof(Vector3), "Vector3"},
        { typeof(Vector4), "Vector4"},
        { typeof(Quaternion), "Vector4"},
        { typeof(object), "Object"}
    };

    private static string TypeChecker(Type t, int stackIdx)
    {
        string s;
        if (ContainsKey(typeCheckerDict, t, out s))
        {
            return string.Format(" && LuaAPI.Is{0}(L, {1})", s, stackIdx);
        }
        else
        {
            Debug.LogError("TypeChecker Error: " + t.FullName);
            return null;
        }
    }

    private static Dictionary<Type, string> typeConverterDict = new Dictionary<Type, string>()
    {
        { typeof(int), "Number"},
        { typeof(float), "Number"},
        { typeof(Enum), "Number"},
        { typeof(Type), "Type"},
        { typeof(bool), "Bool"},
        { typeof(string), "String"},
        { typeof(Vector2), "Vector2"},
        { typeof(Vector3), "Vector3"},
        { typeof(Vector4), "Vector4"},
        { typeof(Quaternion), "Quaternion"},
        { typeof(object), "Object"}
    };

    private static string TypeConverter(Type t, int argIdx, int stackIdx)
    {
        string s;
        if (ContainsKey(typeConverterDict, t, out s))
        {
            return string.Format("{0} arg{1} = ({0})LuaCallback.To{2}(L, {3});", GetTypeString(t), argIdx, s, stackIdx);
        }
        else
        {
            Debug.LogError("TypeConverter Error: " + t.FullName);
            return null;
        }
    }

    private static string CallConverter(MethodInfo funcInfo)
    {
        if (funcInfo.IsStatic)
        {
            if (GetNResults(funcInfo) == 0)
            {
                return string.Format("{0}.{1}({2});", funcInfo.DeclaringType.FullName, funcInfo.Name, GetParamString(funcInfo));
            }
            else
            {
                return string.Format("{0} res = {1}.{2}({3});", GetTypeString(funcInfo.ReturnType), funcInfo.DeclaringType.FullName, funcInfo.Name, GetParamString(funcInfo));
            }
        }
        else
        {
            if (GetNResults(funcInfo) == 0)
            {
                return string.Format("arg0.{0}({1});", funcInfo.Name, GetParamString(funcInfo));
            }
            else
            {
                return string.Format("{0} res = arg0.{1}({2});", GetTypeString(funcInfo.ReturnType), funcInfo.Name, GetParamString(funcInfo));
            }
        }
    }

    private static Dictionary<Type, string> pushConverterDict = new Dictionary<Type, string>()
    {
        { typeof(int),"Number"},
        { typeof(float),"Number"},
        { typeof(Enum), "Number"},
        { typeof(bool),"Bool"},
        { typeof(string),"String"},
        { typeof(Vector2),"Vector"},
        { typeof(Vector3),"Vector"},
        { typeof(Vector4),"Vector"},
        { typeof(Quaternion),"Vector"},
        { typeof(Array),"Array"},
        { typeof(object), "Object"}
    };

    private static string PushConverter(MethodInfo funcInfo)
    {
        if (GetNResults(funcInfo) == 0)
        {
            return null;
        }
        else
        {
            string s;
            if (ContainsKey(pushConverterDict, funcInfo.ReturnType, out s))
            {
                return string.Format("LuaCallback.Push{0}(L, {1}res); ", s, funcInfo.ReturnType.IsEnum ? "(double)" : "");
            }
            else
            {
                Debug.LogError("PushConverter Error: " + funcInfo.ReturnType.FullName);
                return null;
            }
        }
    }

    private static string GetParamString(MethodInfo funcInfo)
    {
        int len = funcInfo.GetParameters().Length;
        if (len == 0)
        {
            return null;
        }
        string res = "";
        for (int i = 0; i < len; i++)
        {
            res += "arg" + (funcInfo.IsStatic ? i : i + 1) + (i == len - 1 ? "" : ", ");
        }
        return res;
    }

    private static string GetTypeString(Type t)
    {
        if (t.IsGenericType)
        {
            Type[] genericParams = t.GetGenericArguments();
            string param = "";
            for (int i = 0; i < genericParams.Length; i++)
            {
                param += genericParams[i].FullName + (i == genericParams.Length - 1 ? "" : ", ");
            }
            int idx = t.FullName.IndexOf('`');
            string name = t.FullName.Substring(0, idx);
            return string.Format("{0}<{1}>", name, param);
        }
        else
        {
            return t.FullName;
        }
    }

    private static int GetNResults(MethodInfo funcInfo)
    {
        return funcInfo.ReturnType == typeof(void) ? 0 : 1;
    }

    private static bool CheckObsolete(MemberInfo memberInfo)
    {
        return memberInfo.GetCustomAttributes(typeof(ObsoleteAttribute), true).Length > 0;
    }
}