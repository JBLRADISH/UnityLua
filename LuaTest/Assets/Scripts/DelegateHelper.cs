using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateHelper<T1, T2, T3, T4>
{

    int reference;

    public DelegateHelper(int reference)
    {
        this.reference = reference;
    }

    public T4 Invoke(T1 arg0, T2 arg1, T3 arg2)
    {
        LuaAPI.PushLuaFunction(LuaEnv.L, reference);
        PushToLua(typeof(T1), arg0);
        PushToLua(typeof(T2), arg1);
        PushToLua(typeof(T3), arg2);
        LuaAPI.CallLuaFunction(LuaEnv.L, 3, 1);
        return PopFromLua();
    }

    void PushToLua(Type type, object arg)
    {
        if (type == typeof(int))
        {
            LuaAPI.PushNumber(LuaEnv.L, (int)arg);
        }
        else if (type == typeof(UnityEngine.Object))
        {
            LuaCallback.PushObject(LuaEnv.L, (UnityEngine.Object)arg);
        }
    }

    T4 PopFromLua()
    {
        if (typeof(T4) == typeof(int))
        {
            return (T4)(object)(int)LuaAPI.ToNumber(LuaEnv.L, -1);
        }
        else
        {
            return default(T4);
        }
    }
}