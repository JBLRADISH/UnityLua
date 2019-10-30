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
                s += LuaAPI.ToStr(L, i);
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
    public static int OpenUrl(IntPtr L)
    {
        if (CheckArgsCount(L, 1))
        {
            if (LuaAPI.IsString(L, 1))
            {
                string arg0 = LuaAPI.ToStr(L, 1);
                UnityEngine.Application.OpenURL(arg0);
            }
        }
        return 0;
    }

    [MonoPInvokeCallback(typeof(LuaCFunction))]
    public static int get_timeScale(IntPtr L)
    {
        Debug.Log(Time.timeScale);
        LuaAPI.PushNumber(L, UnityEngine.Time.timeScale);
        return 1;
    }

    [MonoPInvokeCallback(typeof(LuaCFunction))]
    public static int set_timeScale(IntPtr L)
    {
        if (LuaAPI.IsNumber(L, -1))
        {
            double arg0 = LuaAPI.ToNumber(L, -1);
            UnityEngine.Time.timeScale = (float)arg0;
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    public static int Find(IntPtr L)
    {
        if (CheckArgsCount(L, 1))
        {
            if (LuaAPI.IsString(L, 1))
            {
                string arg0 = LuaAPI.ToStr(L, 1);
                UnityEngine.GameObject o = UnityEngine.GameObject.Find(arg0);
                LuaAPI.PushObj(L, o);
            }
        }
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    public static int get_name(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            UnityEngine.Object o = LuaAPI.ToObj<UnityEngine.Object>(L, 1);
            LuaAPI.PushString(L, o.name);
        }
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    public static int set_name(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, -1))
        {
            UnityEngine.Object o = LuaAPI.ToObj<UnityEngine.Object>(L, 1);
            string arg0 = LuaAPI.ToStr(L, -1);
            o.name = arg0;
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    public static int get_tag(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            UnityEngine.GameObject go = LuaAPI.ToObj<GameObject>(L, 1);
            LuaAPI.PushString(L, go.tag);
        }
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    public static int set_tag(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, -1))
        {
            UnityEngine.GameObject go = LuaAPI.ToObj<GameObject>(L, 1);
            string arg0 = LuaAPI.ToStr(L, -1);
            go.tag = arg0;
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    static int get_transform(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            UnityEngine.GameObject go = LuaAPI.ToObj<GameObject>(L, 1);
            LuaAPI.PushObj(L, go.transform);
        }
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    static int get_position(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            UnityEngine.Transform trans = LuaAPI.ToObj<Transform>(L, 1);
            PushVector3(L, trans.position);
        }
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCFunction))]
    static int set_position(IntPtr L)
    {
        if (LuaAPI.IsObject(L, 1))
        {
            UnityEngine.Transform trans = LuaAPI.ToObj<Transform>(L, 1);
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
        BeginClass("Application", null);
        RegisterFunc("OpenUrl", OpenUrl);
        EndClass();
        BeginClass("Time", null);
        RegisterVar("timeScale", get_timeScale, set_timeScale);
        EndClass();
        BeginClass("Object", null);
        RegisterVar("name", get_name, set_name);
        EndClass();
        BeginClass("Transform", "Object");
        RegisterVar("position", get_position, set_position);
        EndClass();
        BeginClass("GameObject", "Object");
        RegisterFunc("Find", Find);
        RegisterVar("tag", get_tag, set_tag);
        RegisterVar("transform", get_transform, null);
        EndClass();

        BeginClass("HotFix", null);
        RegisterVar("addHotFix", null, set_addHotFix);
        EndClass();
    }

    public static void RegisterLuaFunc(string funcname, LuaCFunction funccallback)
    {
        LuaAPI.RegisterLuaFunc(LuaEnv.L, funcname, Marshal.GetFunctionPointerForDelegate(funccallback));
    }

    public static void BeginClass(string classname, string baseclassname)
    {
        LuaAPI.BeginClass(LuaEnv.L, classname, baseclassname);
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

    public static Vector3 ToVector3(IntPtr L, int idx)
    {
        Vector3 res;
        LuaAPI.ToVector3(L, idx, out res.x, out res.y, out res.z);
        return res;
    }

    public static void PushVector3(IntPtr L, Vector3 v)
    {
        LuaAPI.PushVector3(L, v.x, v.y, v.z);
    }

    public static bool CheckArgsCount(IntPtr L, int count)
    {
        return LuaAPI.GetTop(L) == count;
    }
}