public class ComponentWrap
{
	public static void Register()
	{
		LuaCallback.BeginClass(typeof(UnityEngine.Component), typeof(UnityEngine.Object));
		LuaCallback.RegisterFunc("GetComponent", GetComponent);
		LuaCallback.RegisterFunc("GetComponentInChildren", GetComponentInChildren);
		LuaCallback.RegisterFunc("GetComponentsInChildren", GetComponentsInChildren);
		LuaCallback.RegisterFunc("GetComponentInParent", GetComponentInParent);
		LuaCallback.RegisterFunc("GetComponentsInParent", GetComponentsInParent);
		LuaCallback.RegisterFunc("GetComponents", GetComponents);
		LuaCallback.RegisterFunc("CompareTag", CompareTag);
		LuaCallback.RegisterFunc("SendMessageUpwards", SendMessageUpwards);
		LuaCallback.RegisterFunc("SendMessage", SendMessage);
		LuaCallback.RegisterFunc("BroadcastMessage", BroadcastMessage);
		LuaCallback.RegisterVar("transform", get_transform, null);
		LuaCallback.RegisterVar("gameObject", get_gameObject, null);
		LuaCallback.RegisterVar("tag", get_tag, set_tag);
		LuaCallback.EndClass();
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int GetComponent(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			UnityEngine.Component res = arg0.GetComponent(arg1);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			UnityEngine.Component res = arg0.GetComponent(arg1);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int GetComponentInChildren(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsBool(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			System.Boolean arg2 = (System.Boolean)LuaCallback.ToBool(L, 3);
			UnityEngine.Component res = arg0.GetComponentInChildren(arg1, arg2);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			UnityEngine.Component res = arg0.GetComponentInChildren(arg1);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int GetComponentsInChildren(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsBool(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			System.Boolean arg2 = (System.Boolean)LuaCallback.ToBool(L, 3);
			UnityEngine.Component[] res = arg0.GetComponentsInChildren(arg1, arg2);
			LuaCallback.PushArray(L, res); 
			return 1;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			UnityEngine.Component[] res = arg0.GetComponentsInChildren(arg1);
			LuaCallback.PushArray(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int GetComponentInParent(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			UnityEngine.Component res = arg0.GetComponentInParent(arg1);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int GetComponentsInParent(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsBool(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			System.Boolean arg2 = (System.Boolean)LuaCallback.ToBool(L, 3);
			UnityEngine.Component[] res = arg0.GetComponentsInParent(arg1, arg2);
			LuaCallback.PushArray(L, res); 
			return 1;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			UnityEngine.Component[] res = arg0.GetComponentsInParent(arg1);
			LuaCallback.PushArray(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int GetComponents(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			UnityEngine.Component[] res = arg0.GetComponents(arg1);
			LuaCallback.PushArray(L, res); 
			return 1;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsObject(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
			System.Collections.Generic.List<UnityEngine.Component> arg2 = (System.Collections.Generic.List<UnityEngine.Component>)LuaCallback.ToObject(L, 3);
			arg0.GetComponents(arg1, arg2);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int CompareTag(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Boolean res = arg0.CompareTag(arg1);
			LuaCallback.PushBool(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int SendMessageUpwards(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
			UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 4);
			arg0.SendMessageUpwards(arg1, arg2, arg3);
			
			return 0;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
			arg0.SendMessageUpwards(arg1, arg2);
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			arg0.SendMessageUpwards(arg1);
			
			return 0;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsNumber(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 3);
			arg0.SendMessageUpwards(arg1, arg2);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int SendMessage(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
			arg0.SendMessage(arg1, arg2);
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			arg0.SendMessage(arg1);
			
			return 0;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
			UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 4);
			arg0.SendMessage(arg1, arg2, arg3);
			
			return 0;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsNumber(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 3);
			arg0.SendMessage(arg1, arg2);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int BroadcastMessage(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
			UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 4);
			arg0.BroadcastMessage(arg1, arg2, arg3);
			
			return 0;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
			arg0.BroadcastMessage(arg1, arg2);
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			arg0.BroadcastMessage(arg1);
			
			return 0;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsNumber(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 3);
			arg0.BroadcastMessage(arg1, arg2);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_transform(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			UnityEngine.Transform res = arg0.transform;
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_gameObject(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			UnityEngine.GameObject res = arg0.gameObject;
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_tag(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String res = arg0.tag;
			LuaCallback.PushString(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_tag(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 3))
		{
			UnityEngine.Component arg0 = (UnityEngine.Component)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 3);
			arg0.tag = arg1;
			return 0;
		}
		return 0;
	}
}
