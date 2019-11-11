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

    public int Invoke(int arg1, int arg2)
    {
        LuaAPI.PushLuaFunction(LuaEnv.L, reference);
        LuaCallback.PushNumber(LuaEnv.L, arg1);
        LuaCallback.PushNumber(LuaEnv.L, arg2);
        LuaAPI.CallLuaFunction(LuaEnv.L, 2, 1);
        return (int)LuaAPI.ToNumber(LuaEnv.L, -1);
    }

}