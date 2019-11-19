public class MonoBehaviourWrap
{
	public static void Register()
	{
		LuaCallback.BeginClass(typeof(UnityEngine.MonoBehaviour), typeof(UnityEngine.Behaviour));
		LuaCallback.RegisterFunc("IsInvoking", IsInvoking);
		LuaCallback.RegisterFunc("CancelInvoke", CancelInvoke);
		LuaCallback.RegisterFunc("Invoke", Invoke);
		LuaCallback.RegisterFunc("InvokeRepeating", InvokeRepeating);
		LuaCallback.RegisterFunc("StartCoroutine", StartCoroutine);
		LuaCallback.RegisterFunc("StopCoroutine", StopCoroutine);
		LuaCallback.RegisterFunc("StopAllCoroutines", StopAllCoroutines);
		LuaCallback.RegisterFunc("print", print);
		LuaCallback.RegisterVar("useGUILayout", get_useGUILayout, set_useGUILayout);
		LuaCallback.RegisterVar("runInEditMode", get_runInEditMode, set_runInEditMode);
		LuaCallback.EndClass();
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int IsInvoking(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 1 && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.Boolean res = arg0.IsInvoking();
			LuaCallback.PushBool(L, res); 
			return 1;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Boolean res = arg0.IsInvoking(arg1);
			LuaCallback.PushBool(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int CancelInvoke(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 1 && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			arg0.CancelInvoke();
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			arg0.CancelInvoke(arg1);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int Invoke(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsNumber(L, 3))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			arg0.Invoke(arg1, arg2);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int InvokeRepeating(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			arg0.InvokeRepeating(arg1, arg2, arg3);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int StartCoroutine(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			UnityEngine.Coroutine res = arg0.StartCoroutine(arg1);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
			UnityEngine.Coroutine res = arg0.StartCoroutine(arg1, arg2);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.Collections.IEnumerator arg1 = (System.Collections.IEnumerator)LuaCallback.ToObject(L, 2);
			UnityEngine.Coroutine res = arg0.StartCoroutine(arg1);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int StopCoroutine(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.Collections.IEnumerator arg1 = (System.Collections.IEnumerator)LuaCallback.ToObject(L, 2);
			arg0.StopCoroutine(arg1);
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			UnityEngine.Coroutine arg1 = (UnityEngine.Coroutine)LuaCallback.ToObject(L, 2);
			arg0.StopCoroutine(arg1);
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			arg0.StopCoroutine(arg1);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int StopAllCoroutines(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 1 && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			arg0.StopAllCoroutines();
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int print(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 1 && LuaAPI.IsObject(L, 1))
		{
			System.Object arg0 = (System.Object)LuaCallback.ToObject(L, 1);
			UnityEngine.MonoBehaviour.print(arg0);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_useGUILayout(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.Boolean res = arg0.useGUILayout;
			LuaCallback.PushBool(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_runInEditMode(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.Boolean res = arg0.runInEditMode;
			LuaCallback.PushBool(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_useGUILayout(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsBool(L, 3))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.Boolean arg1 = (System.Boolean)LuaCallback.ToBool(L, 3);
			arg0.useGUILayout = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_runInEditMode(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsBool(L, 3))
		{
			UnityEngine.MonoBehaviour arg0 = (UnityEngine.MonoBehaviour)LuaCallback.ToObject(L, 1);
			System.Boolean arg1 = (System.Boolean)LuaCallback.ToBool(L, 3);
			arg0.runInEditMode = arg1;
			return 0;
		}
		return 0;
	}
}
