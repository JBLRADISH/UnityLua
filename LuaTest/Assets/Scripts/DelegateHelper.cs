using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateHelper
{

    int reference;

    public DelegateHelper(int reference)
    {
        this.reference = reference;
    }

    public int Invoke(object arg0, int arg1, int arg2)
    {
        LuaAPI.PushLuaFunction(LuaEnv.L, reference);
        LuaCallback.PushObject(LuaEnv.L, arg0);
        LuaAPI.PushNumber(LuaEnv.L, arg1);
        LuaAPI.PushNumber(LuaEnv.L, arg2);
        LuaAPI.CallLuaFunction(LuaEnv.L, 3, 1);
        return (int)LuaAPI.ToNumber(LuaEnv.L, -1);
    }

}