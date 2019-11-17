public class DelegateHelper
{
	int reference;
	System.IntPtr L;
	public DelegateHelper(int reference)
	{
		this.reference = reference;
		L = LuaEnv.L;
	}
	public System.Int32 Invoke_HotFix_Int32_Int32_Int32(HotFix arg0, System.Int32 arg1, System.Int32 arg2)
	{
		LuaAPI.PushLuaFunction(L, reference);
		LuaCallback.PushObject(L, arg0); 
		LuaCallback.PushNumber(L, arg1); 
		LuaCallback.PushNumber(L, arg2); 
		LuaAPI.CallLuaFunction(L, 3, 1);
		System.Int32 arg3 = (System.Int32)LuaCallback.ToNumber(L, -1);
		return arg3;
	}
}
