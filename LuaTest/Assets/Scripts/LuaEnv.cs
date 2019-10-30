using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaEnv : IDisposable
{
    public static IntPtr L { get; private set; }

    public LuaEnv()
    {
        L = LuaAPI.Init();
        LuaCallback.Register();
        AddResearchPath();
    }

    public void DoString(string s)
    {
        LuaAPI.DoString(L, s);
    }

    public void DoFile(string filename)
    {
        string realFilePath = LuaFileUtils.Instance.GetFilePath(filename);
        if (!string.IsNullOrEmpty(realFilePath))
        {
            LuaAPI.DoFile(L, realFilePath);
        }
    }

    private void AddResearchPath()
    {
        AddResearchPath(Application.dataPath + "/Lua/?.lua");
    }

    private void AddResearchPath(string filepath)
    {
        LuaFileUtils.Instance.AddResearchPath(filepath);
        LuaAPI.AddResearchPath(L, filepath);
    }

    public void Dispose()
    {
        LuaAPI.Close(L);
    }
}
