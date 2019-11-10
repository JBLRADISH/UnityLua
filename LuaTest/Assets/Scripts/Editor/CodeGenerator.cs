using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Reflection;
using System;
using System.Linq;

public class VarInfo
{
    public string Name;
    public bool IsGetStatic;
    public bool IsSetStatic;
    public Type VarType;
    public Type DeclaringType;

    public VarInfo(FieldInfo fieldInfo)
    {
        Name = fieldInfo.Name;
        IsGetStatic = fieldInfo.IsStatic;
        IsSetStatic = fieldInfo.IsStatic;
        VarType = fieldInfo.FieldType;
        DeclaringType = fieldInfo.DeclaringType;
    }

    public VarInfo(PropertyInfo propertyInfo)
    {
        Name = propertyInfo.Name;
        if (propertyInfo.GetGetMethod() != null)
        {
            IsGetStatic = propertyInfo.GetGetMethod().IsStatic;
        }
        if (propertyInfo.GetSetMethod() != null)
        {
            IsSetStatic = propertyInfo.GetSetMethod().IsStatic;
        }
        VarType = propertyInfo.PropertyType;
        DeclaringType = propertyInfo.DeclaringType;
    }
}

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
        List<Type> list = new List<Type>();
        for (int i = 0; i < WrapSettings.wrapClasses.Count; i++)
        {
            WrapClassGenerator(WrapSettings.wrapClasses[i], list);
        }
        FileStream fs = new FileStream(Application.dataPath + "/Scripts/LuaBinder.cs", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("public class LuaBinder");
        sw.WriteLine("{");
        sw.WriteLine("public static void Register()");
        sw.WriteLine("{");
        for (int i = 0; i < list.Count; i++)
        {
            sw.WriteLine(string.Format("{0}Wrap.Register();", list[i].Name));
        }
        sw.WriteLine("}");
        sw.WriteLine("}");
        sw.Close();
        fs.Close();
        FileUtils.FileFormatter(Application.dataPath + "/Scripts/LuaBinder.cs");
        AssetDatabase.Refresh();
    }

    private static void WrapClassGenerator(Type t, List<Type> list)
    {
        if (t != typeof(UnityEngine.Object))
        {
            WrapClassGenerator(t.BaseType, list);
        }
        if (list.Contains(t))
        {
            return;
        }
        list.Add(t);
        string filePath = Application.dataPath + "/Scripts/Wrap/" + t.Name + "Wrap.cs";
        FileStream fs = new FileStream(filePath, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("public class " + t.Name + "Wrap");
        sw.WriteLine("{");
        WrapRegisterGenerator(t, sw);
        sw.WriteLine("}");
        sw.Close();
        fs.Close();
        FileUtils.FileFormatter(filePath);
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
        FieldInfo[] fieldInfos = t.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        List<VarInfo> varGetInfos = new List<VarInfo>();
        List<VarInfo> varSetInfos = new List<VarInfo>();
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            FieldInfo fieldInfo = fieldInfos[i];
            if (!CheckObsolete(fieldInfo))
            {
                sw.WriteLine(string.Format("LuaCallback.RegisterVar(\"{0}\", get_{0}, {1});", fieldInfo.Name, fieldInfo.IsLiteral ? "null" : "set_" + fieldInfo.Name));
                VarInfo varInfo = new VarInfo(fieldInfo);
                varGetInfos.Add(varInfo);
                if (!fieldInfo.IsLiteral)
                {
                    varSetInfos.Add(varInfo);
                }
            }
        }
        PropertyInfo[] propertyInfos = t.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            PropertyInfo propertyInfo = propertyInfos[i];
            if (!CheckObsolete(propertyInfo))
            {
                sw.WriteLine(string.Format("LuaCallback.RegisterVar(\"{0}\", {1}, {2});", propertyInfo.Name, propertyInfo.CanRead ? "get_" + propertyInfo.Name : "null", propertyInfo.CanWrite ? "set_" + propertyInfo.Name : "null"));
                VarInfo varInfo = new VarInfo(propertyInfo);
                if (propertyInfo.CanRead)
                {
                    varGetInfos.Add(varInfo);
                }
                if (propertyInfo.CanWrite)
                {
                    varSetInfos.Add(varInfo);
                }
            }
        }
        sw.WriteLine("LuaCallback.EndClass();");
        sw.WriteLine("}");

        foreach (var kv in funcInfos)
        {
            WrapFuncGenerator(sw, kv.Value);
        }

        foreach (var item in varGetInfos)
        {
            WrapVarGetGenerator(sw, item);
        }

        foreach (var item in varSetInfos)
        {
            WrapVarSetGenerator(sw, item);
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

    private static void WrapVarGetGenerator(StreamWriter sw, VarInfo varInfo)
    {
        sw.WriteLine("");
        sw.WriteLine("[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]");
        sw.WriteLine(string.Format("public static int get_{0}(System.IntPtr L)", varInfo.Name));
        sw.WriteLine("{");
        string condition = varInfo.IsGetStatic ? null : string.Format("if (true{0})", TypeChecker(typeof(object), 1));
        sw.WriteLine(condition);
        sw.WriteLine("{");
        sw.WriteLine(varInfo.IsGetStatic ? null : TypeConverter(varInfo.DeclaringType, 0, 1));
        sw.WriteLine(CallConverter(varInfo, true));
        sw.WriteLine(PushConverter(varInfo));
        sw.WriteLine(string.Format("return 1;"));
        sw.WriteLine("}");
        sw.WriteLine("return 0;");
        sw.WriteLine("}");
    }

    private static void WrapVarSetGenerator(StreamWriter sw, VarInfo varInfo)
    {
        sw.WriteLine("");
        sw.WriteLine("[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]");
        sw.WriteLine(string.Format("public static int set_{0}(System.IntPtr L)", varInfo.Name));
        sw.WriteLine("{");
        string condition = string.Format("if (true{0}{1})", varInfo.IsSetStatic ? null : TypeChecker(typeof(object), 1), TypeChecker(varInfo.VarType, 3));
        sw.WriteLine(condition);
        sw.WriteLine("{");
        sw.WriteLine(varInfo.IsSetStatic ? null : TypeConverter(varInfo.DeclaringType, 0, 1));
        sw.WriteLine(TypeConverter(varInfo.VarType, varInfo.IsSetStatic ? 0 : 1, 3));
        sw.WriteLine(CallConverter(varInfo, false));
        sw.WriteLine(string.Format("return 0;"));
        sw.WriteLine("}");
        sw.WriteLine("return 0;");
        sw.WriteLine("}");
    }

    private static bool ContainsKey(Dictionary<Type, string> dict, Type t, out string s)
    {
        Type tmp = t;
        while (tmp != null)
        {
            if (tmp.IsInterface)
            {
                s = dict[typeof(object)];
                return true;
            }
            if (dict.ContainsKey(tmp))
            {
                s = dict[tmp];
                return true;
            }
            tmp = tmp.BaseType;
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

    private static string CallConverter(VarInfo varInfo, bool isGet)
    {
        if (isGet)
        {
            if (varInfo.IsGetStatic)
            {
                return string.Format("{0} res = {1}.{2};", GetTypeString(varInfo.VarType), varInfo.DeclaringType.FullName, varInfo.Name);
            }
            else
            {
                return string.Format("{0} res = arg0.{1};", GetTypeString(varInfo.VarType), varInfo.Name);
            }
        }
        else
        {
            if (varInfo.IsSetStatic)
            {
                return string.Format("{0}.{1} = arg0;", varInfo.DeclaringType.FullName, varInfo.Name);
            }
            else
            {
                return string.Format("arg0.{0} = arg1;", varInfo.Name);
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

    private static string PushConverter(VarInfo varInfo)
    {
        string s;
        if (ContainsKey(pushConverterDict, varInfo.VarType, out s))
        {
            return string.Format("LuaCallback.Push{0}(L, {1}res); ", s, varInfo.VarType.IsEnum ? "(double)" : "");
        }
        else
        {
            Debug.LogError("PushConverter Error: " + varInfo.VarType.FullName);
            return null;
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