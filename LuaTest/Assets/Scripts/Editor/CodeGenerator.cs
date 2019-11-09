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

    private static string TypeChecker(Type t, int stackIdx)
    {
        if (t == typeof(int) || t == typeof(float) || t == typeof(Type) || t.IsEnum)
        {
            return string.Format(" && LuaAPI.IsNumber(L, {0})", stackIdx);
        }
        else if (t == typeof(bool))
        {
            return string.Format(" && LuaAPI.IsBool(L, {0})", stackIdx);
        }
        else if (t == typeof(string))
        {
            return string.Format(" && LuaAPI.IsString(L, {0})", stackIdx);
        }
        else if (t == typeof(Vector2) || t == typeof(Vector3) || t == typeof(Vector4) || t == typeof(Quaternion))
        {
            return null;
        }
        else
        {
            return string.Format(" && LuaAPI.IsObject(L, {0})", stackIdx);
        }
    }

    private static string TypeConverter(Type t, int argIdx, int stackIdx)
    {
        if (t == typeof(int) || t == typeof(float) || t.IsEnum)
        {
            return string.Format("{0} arg{1} = ({0})LuaAPI.ToNumber(L, {2});", t.FullName, argIdx, stackIdx);
        }
        else if (t == typeof(Type))
        {
            return string.Format("{0} arg{1} = LuaCallback.ToType(L, {2});", t.FullName, argIdx, stackIdx);
        }
        else if (t == typeof(bool))
        {
            return string.Format("{0} arg{1} = LuaAPI.ToBool(L, {2});", t.FullName, argIdx, stackIdx);
        }
        else if (t == typeof(string))
        {
            return string.Format("{0} arg{1} = LuaCallback.ToString(L, {2});", t.FullName, argIdx, stackIdx);
        }
        else if (t == typeof(Vector2))
        {
            return string.Format("{0} arg{1} = LuaCallback.ToVector2(L, {2});", t.FullName, argIdx, stackIdx);
        }
        else if (t == typeof(Vector3))
        {
            return string.Format("{0} arg{1} = LuaCallback.ToVector3(L, {2});", t.FullName, argIdx, stackIdx);
        }
        else if (t == typeof(Vector4))
        {
            return string.Format("{0} arg{1} = LuaCallback.ToVector4(L, {2});", t.FullName, argIdx, stackIdx);
        }
        else if (t == typeof(Quaternion))
        {
            return string.Format("{0} arg{1} = LuaCallback.ToQuaternion(L, {2});", t.FullName, argIdx, stackIdx);
        }
        else
        {
            return string.Format("{0} arg{1} = LuaCallback.ToObject<{0}>(L, {2});", GetTypeString(t), argIdx, stackIdx);
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

    private static string PushConverter(MethodInfo funcInfo)
    {
        if (GetNResults(funcInfo) == 0)
        {
            return null;
        }
        else
        {
            if (funcInfo.ReturnType == typeof(int) || funcInfo.ReturnType == typeof(float) || funcInfo.ReturnType.IsEnum)
            {
                return "LuaAPI.PushNumber(L, (double)res);";
            }
            else if (funcInfo.ReturnType == typeof(bool))
            {
                return "LuaAPI.PushBool(L, res);";
            }
            else if (funcInfo.ReturnType == typeof(string))
            {
                return "LuaAPI.PushString(L, res);";
            }
            else if (funcInfo.ReturnType == typeof(Vector2) || funcInfo.ReturnType == typeof(Vector3) || funcInfo.ReturnType == typeof(Vector4) || funcInfo.ReturnType == typeof(Quaternion))
            {
                return "LuaCallback.PushVector(L, res);";
            }
            else if (funcInfo.ReturnType.IsArray)
            {
                return "LuaCallback.PushArray(L, res);";
            }
            else
            {
                return "LuaCallback.PushObject(L, res);";
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