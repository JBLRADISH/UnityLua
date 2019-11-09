using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LuaAPI
{

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Init();

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void DoString(IntPtr L, string s);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void AddResearchPath(IntPtr L, string filepath);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void DoFile(IntPtr L, string filename);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Close(IntPtr L);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetTop(IntPtr L);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsString(IntPtr L, int i);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr ToString(IntPtr L, int i);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PushString(IntPtr L, string s);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsNumber(IntPtr L, int i);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern double ToNumber(IntPtr L, int i);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PushNumber(IntPtr L, double d);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsBool(IntPtr L, int i);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool ToBool(IntPtr L, int i);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PushBool(IntPtr L, bool b);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsObject(IntPtr L, int idx);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ToObject(IntPtr L, int idx);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PushObject(IntPtr L, string classname, int obj);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ToVector2(IntPtr L, int idx, out float x, out float y);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PushVector2(IntPtr L, float x, float y);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ToVector3(IntPtr L, int idx, out float x, out float y, out float z);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PushVector3(IntPtr L, float x, float y, float z);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ToVector4(IntPtr L, int idx, out float x, out float y, out float z, out float w);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PushVector4(IntPtr L, float x, float y, float z, float w);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsLuaFunction(IntPtr L, int idx);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ToLuaFunction(IntPtr L);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PushLuaFunction(IntPtr L, int reference);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void CallLuaFunction(IntPtr L, int nargs, int nresults);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void RawSetI(IntPtr L, int idx, int i);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void NewTable(IntPtr L);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void RegisterLuaFunc(IntPtr L, string funcname, IntPtr funcptr);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void BeginClass(IntPtr L, string classname, string baseclassname, int idx);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void RegisterFunc(IntPtr L, string funcname, IntPtr funcptr);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void RegisterVar(IntPtr L, string varname, IntPtr getptr, IntPtr setptr);

    [DllImport("LuaDLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void EndClass(IntPtr L);
}