public class HotFixWrap
{
	public static void Register()
	{
		LuaCallback.BeginClass(typeof(HotFix), typeof(UnityEngine.MonoBehaviour));
		LuaCallback.RegisterFunc("Add", Add);
		LuaCallback.RegisterVar("AddHotFix", null, set_AddHotFix);
		LuaCallback.EndClass();
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_AddHotFix(System.IntPtr L)
	{
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int Add(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3))
		{
			HotFix arg0 = (HotFix)LuaCallback.ToObject(L, 1);
			System.Int32 arg1 = (System.Int32)LuaCallback.ToNumber(L, 2);
			System.Int32 arg2 = (System.Int32)LuaCallback.ToNumber(L, 3);
			System.Int32 res = arg0.Add(arg1, arg2);
			LuaCallback.PushNumber(L, res); 
			return 1;
		}
		return 0;
	}
}
