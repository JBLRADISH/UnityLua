public static class ObjectWrap
{
    public static void Register()
    {
        LuaCallback.BeginClass(typeof(UnityEngine.Object), null);
        LuaCallback.RegisterFunc("GetInstanceID", GetInstanceID);
        LuaCallback.RegisterFunc("GetHashCode", GetHashCode);
        LuaCallback.RegisterFunc("Equals", Equals);
        LuaCallback.RegisterFunc("Instantiate", Instantiate);
        LuaCallback.RegisterFunc("Destroy", Destroy);
        LuaCallback.RegisterFunc("DestroyImmediate", DestroyImmediate);
        LuaCallback.RegisterFunc("FindObjectsOfType", FindObjectsOfType);
        LuaCallback.RegisterFunc("DontDestroyOnLoad", DontDestroyOnLoad);
        LuaCallback.RegisterFunc("FindObjectOfType", FindObjectOfType);
        LuaCallback.RegisterFunc("ToString", ToString);
        LuaCallback.EndClass();
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int GetInstanceID(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 1 && LuaAPI.IsObject(L, 1))
        {
            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            System.Int32 res = arg0.GetInstanceID();
            LuaAPI.PushNumber(L, (double)res);
            return 1;
        }
        return 0;
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int GetHashCode(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 1 && LuaAPI.IsObject(L, 1))
        {
            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            System.Int32 res = arg0.GetHashCode();
            LuaAPI.PushNumber(L, (double)res);
            return 1;
        }
        return 0;
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int Equals(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2))
        {
            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            System.Object arg1 = LuaCallback.ToObject<System.Object>(L, 2);
            System.Boolean res = arg0.Equals(arg1);
            LuaAPI.PushBool(L, res);
            return 1;
        }
        return 0;
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int Instantiate(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 3 && LuaAPI.IsObject(L, 1))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            UnityEngine.Vector3 arg1 = LuaCallback.ToVector3(L, 2);
            UnityEngine.Quaternion arg2 = LuaCallback.ToQuaternion(L, 3);
            UnityEngine.Object res = UnityEngine.Object.Instantiate(arg0, arg1, arg2);
            LuaCallback.PushObject(L, res);
            return 1;
        }
        if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 4))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            UnityEngine.Vector3 arg1 = LuaCallback.ToVector3(L, 2);
            UnityEngine.Quaternion arg2 = LuaCallback.ToQuaternion(L, 3);
            UnityEngine.Transform arg3 = LuaCallback.ToObject<UnityEngine.Transform>(L, 4);
            UnityEngine.Object res = UnityEngine.Object.Instantiate(arg0, arg1, arg2, arg3);
            LuaCallback.PushObject(L, res);
            return 1;
        }
        if (nargs == 1 && LuaAPI.IsObject(L, 1))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            UnityEngine.Object res = UnityEngine.Object.Instantiate(arg0);
            LuaCallback.PushObject(L, res);
            return 1;
        }
        if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            UnityEngine.Transform arg1 = LuaCallback.ToObject<UnityEngine.Transform>(L, 2);
            UnityEngine.Object res = UnityEngine.Object.Instantiate(arg0, arg1);
            LuaCallback.PushObject(L, res);
            return 1;
        }
        if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2) && LuaAPI.IsBool(L, 3))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            UnityEngine.Transform arg1 = LuaCallback.ToObject<UnityEngine.Transform>(L, 2);
            System.Boolean arg2 = LuaAPI.ToBool(L, 3);
            UnityEngine.Object res = UnityEngine.Object.Instantiate(arg0, arg1, arg2);
            LuaCallback.PushObject(L, res);
            return 1;
        }
        return 0;
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int Destroy(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            System.Single arg1 = (System.Single)LuaAPI.ToNumber(L, 2);
            UnityEngine.Object.Destroy(arg0, arg1);

            return 0;
        }
        if (nargs == 1 && LuaAPI.IsObject(L, 1))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            UnityEngine.Object.Destroy(arg0);

            return 0;
        }
        return 0;
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int DestroyImmediate(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsBool(L, 2))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            System.Boolean arg1 = LuaAPI.ToBool(L, 2);
            UnityEngine.Object.DestroyImmediate(arg0, arg1);

            return 0;
        }
        if (nargs == 1 && LuaAPI.IsObject(L, 1))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            UnityEngine.Object.DestroyImmediate(arg0);

            return 0;
        }
        return 0;
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int FindObjectsOfType(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 1 && LuaAPI.IsNumber(L, 1))
        {

            System.Type arg0 = LuaCallback.ToType(L, 1);
            UnityEngine.Object[] res = UnityEngine.Object.FindObjectsOfType(arg0);
            LuaCallback.PushArray(L, res);
            return 1;
        }
        return 0;
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int DontDestroyOnLoad(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 1 && LuaAPI.IsObject(L, 1))
        {

            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            UnityEngine.Object.DontDestroyOnLoad(arg0);

            return 0;
        }
        return 0;
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int FindObjectOfType(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 1 && LuaAPI.IsNumber(L, 1))
        {

            System.Type arg0 = LuaCallback.ToType(L, 1);
            UnityEngine.Object res = UnityEngine.Object.FindObjectOfType(arg0);
            LuaCallback.PushObject(L, res);
            return 1;
        }
        return 0;
    }

    [AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
    public static int ToString(System.IntPtr L)
    {
        int nargs = LuaAPI.GetTop(L);
        if (nargs == 1 && LuaAPI.IsObject(L, 1))
        {
            UnityEngine.Object arg0 = LuaCallback.ToObject<UnityEngine.Object>(L, 1);
            System.String res = arg0.ToString();
            LuaAPI.PushString(L, res);
            return 1;
        }
        return 0;
    }
}