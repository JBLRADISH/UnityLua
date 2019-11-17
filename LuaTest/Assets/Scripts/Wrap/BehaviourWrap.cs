public class BehaviourWrap
{
	public static void Register()
	{
		LuaCallback.BeginClass(typeof(UnityEngine.Behaviour), typeof(UnityEngine.Component));
		LuaCallback.RegisterVar("enabled", get_enabled, set_enabled);
		LuaCallback.RegisterVar("isActiveAndEnabled", get_isActiveAndEnabled, null);
		LuaCallback.EndClass();
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_enabled(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Behaviour arg0 = (UnityEngine.Behaviour)LuaCallback.ToObject(L, 1);
			System.Boolean res = arg0.enabled;
			LuaCallback.PushBool(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_isActiveAndEnabled(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Behaviour arg0 = (UnityEngine.Behaviour)LuaCallback.ToObject(L, 1);
			System.Boolean res = arg0.isActiveAndEnabled;
			LuaCallback.PushBool(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_enabled(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsBool(L, 3))
		{
			UnityEngine.Behaviour arg0 = (UnityEngine.Behaviour)LuaCallback.ToObject(L, 1);
			System.Boolean arg1 = (System.Boolean)LuaCallback.ToBool(L, 3);
			arg0.enabled = arg1;
			return 0;
		}
		return 0;
	}
}
