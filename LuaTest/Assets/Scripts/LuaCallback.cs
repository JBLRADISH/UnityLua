using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

 public static class LuaCallback
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int LuaCFunction(IntPtr L);

    [MonoPInvokeCallback(typeof(LuaCFunction))]
    public static int Print(IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        string s = "LUA: ";
        for (int i = 1; i <= nargs; i++)
        {
            if (LuaAPI.IsString(L, i))
            {
                s += ToString(L, i);
                if (i != nargs)
                {
                    s += "\t";
                }
            }
        }
        Debug.Log(s);
        return 0;
    }

    [MonoPInvokeCallback(typeof(LuaCFunction))]
    public static int LuaGC(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            int idx = LuaAPI.ToObject(L, 1);
            ObjectTranslator.Instance.PopObj(idx);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    public static int get_tag(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            UnityEngine.GameObject go = ToObject<GameObject>(L, 1);
            LuaAPI.PushString(L, go.tag);
        }
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    public static int set_tag(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, -1))
        {
            UnityEngine.GameObject go = ToObject<GameObject>(L, 1);
            string arg0 = ToString(L, -1);
            go.tag = arg0;
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    static int get_transform(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            UnityEngine.GameObject go = ToObject<GameObject>(L, 1);
            PushObject(L, go.transform);
        }
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    static int get_position(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            UnityEngine.Transform trans = ToObject<Transform>(L, 1);
            PushVector(L, trans.position);
        }
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    static int set_position(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            UnityEngine.Transform trans = ToObject<Transform>(L, 1);
            trans.position = ToVector3(L, -1);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    static int set_addHotFix(IntPtr L)
    {
        if (LuaAPI.IsLuaFunction(L, -1))
        {
            int reference = LuaAPI.ToLuaFunction(L);
            HotFix.addHotFix = new DelegateHelper<UnityEngine.Object, int, int, int>(reference);
        }
        return 0;
    }

    public static void Register()
    {
        RegisterLuaFunc("print", Print);
        RegisterLuaFunc("gc", LuaGC);

        ObjectWrap.Register();
        GameObjectWrap.Register();
        BeginClass(typeof(Transform), typeof(UnityEngine.Object));
        RegisterVar("position", get_position, set_position);
        EndClass();
        //BeginClass(typeof(GameObject), typeof(UnityEngine.Object));
        //RegisterVar("tag", get_tag, set_tag);
        //RegisterVar("transform", get_transform, null);
        //EndClass();

        //BeginClass(typeof(HotFix), null);
        //RegisterVar("addHotFix", null, set_addHotFix);
        //EndClass();
    }

    public static void RegisterLuaFunc(string funcname, LuaCFunction funccallback)
    {
        LuaAPI.RegisterLuaFunc(LuaEnv.L, funcname, Marshal.GetFunctionPointerForDelegate(funccallback));
    }

    public static void BeginClass(Type classType, Type baseclassType)
    {
        int idx = ObjectTranslator.Instance.PushObj(classType);
        LuaAPI.BeginClass(LuaEnv.L, classType.Name, baseclassType != null ? baseclassType.Name : null, idx);
    }

    public static void RegisterFunc(string funcname, LuaCFunction funccallback)
    {
        LuaAPI.RegisterFunc(LuaEnv.L, funcname, Marshal.GetFunctionPointerForDelegate(funccallback));
    }

    public static void RegisterVar(string varname, LuaCFunction getcallback, LuaCFunction setcallback)
    {
        IntPtr getptr = getcallback != null ? Marshal.GetFunctionPointerForDelegate(getcallback) : IntPtr.Zero;
        IntPtr setptr = setcallback != null ? Marshal.GetFunctionPointerForDelegate(setcallback) : IntPtr.Zero;
        LuaAPI.RegisterVar(LuaEnv.L, varname, getptr, setptr);
    }

    public static void EndClass()
    {
        LuaAPI.EndClass(LuaEnv.L);
    }

    public static string ToString(IntPtr L, int i)
    {
        IntPtr str = LuaAPI.ToString(L, i);
        return Marshal.PtrToStringAnsi(str);
    }

    public static T ToObject<T>(IntPtr L, int idx)
    {
        int index = LuaAPI.ToObject(L, idx);
        return ObjectTranslator.Instance.Get<T>(index);
    }

    public static Type ToType(IntPtr L, int idx)
    {
        int index = (int)LuaAPI.ToNumber(L, idx);
        return ObjectTranslator.Instance.Get<Type>(index);
    }

    public static void PushObject(IntPtr L, object t)
    {
        LuaAPI.PushObject(L, t.GetType().Name, ObjectTranslator.Instance.PushObj(t));
    }

    public static Vector2 ToVector2(IntPtr L, int idx)
    {
        Vector2 res;
        LuaAPI.ToVector2(L, idx, out res.x, out res.y);
        return res;
    }

    public static Vector3 ToVector3(IntPtr L, int idx)
    {
        Vector3 res;
        LuaAPI.ToVector3(L, idx, out res.x, out res.y, out res.z);
        return res;
    }

    public static Vector4 ToVector4(IntPtr L, int idx)
    {
        Vector4 res;
        LuaAPI.ToVector4(L, idx, out res.x, out res.y, out res.z, out res.w);
        return res;
    }

    public static Quaternion ToQuaternion(IntPtr L, int idx)
    {
        Quaternion res;
        LuaAPI.ToVector4(L, idx, out res.x, out res.y, out res.z, out res.w);
        return res;
    }

    public static void PushVector(IntPtr L, Vector2 v)
    {
        LuaAPI.PushVector2(L, v.x, v.y);
    }

    public static void PushVector(IntPtr L, Vector3 v)
    {
        LuaAPI.PushVector3(L, v.x, v.y, v.z);
    }

    public static void PushVector(IntPtr L, Vector4 v)
    {
        LuaAPI.PushVector4(L, v.x, v.y, v.z, v.w);
    }

    public static void PushVector(IntPtr L, Quaternion q)
    {
        LuaAPI.PushVector4(L, q.x, q.y, q.z, q.w);
    }

    public static void PushArray(IntPtr L, int[] array)
    {
        LuaAPI.NewTable(L);
        LuaAPI.PushNumber(L, -1);
        LuaAPI.RawSetI(L, -2, 0);
        for (int i = 0; i < array.Length; i++)
        {
            LuaAPI.PushNumber(L, array[i]);
            LuaAPI.RawSetI(L, -2, i + 1);
        }
    }

    public static void PushArray(IntPtr L, float[] array)
    {
        LuaAPI.NewTable(L);
        LuaAPI.PushNumber(L, -1);
        LuaAPI.RawSetI(L, -2, 0);
        for (int i = 0; i < array.Length; i++)
        {
            LuaAPI.PushNumber(L, array[i]);
            LuaAPI.RawSetI(L, -2, i + 1);
        }
    }

    public static void PushArray(IntPtr L, string[] array)
    {
        LuaAPI.NewTable(L);
        LuaAPI.PushNumber(L, -1);
        LuaAPI.RawSetI(L, -2, 0);
        for (int i = 0; i < array.Length; i++)
        {
            LuaAPI.PushString(L, array[i]);
            LuaAPI.RawSetI(L, -2, i + 1);
        }
    }

    public static void PushArray(IntPtr L, object[] array)
    {
        LuaAPI.NewTable(L);
        LuaAPI.PushNumber(L, -1);
        LuaAPI.RawSetI(L, -2, 0);
        for (int i = 0; i < array.Length; i++)
        {
            PushObject(L, array[i]);
            LuaAPI.RawSetI(L, -2, i + 1);
        }
    }

    public static bool CheckArgsCount(IntPtr L, int count)
    {
        return LuaAPI.GetTop(L) == count;
    }
}