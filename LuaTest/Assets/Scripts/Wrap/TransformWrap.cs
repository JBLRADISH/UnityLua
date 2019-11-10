public class TransformWrap
{
	public static void Register()
	{
		LuaCallback.BeginClass(typeof(UnityEngine.Transform), typeof(UnityEngine.Component));
		LuaCallback.RegisterFunc("SetParent", SetParent);
		LuaCallback.RegisterFunc("SetPositionAndRotation", SetPositionAndRotation);
		LuaCallback.RegisterFunc("Translate", Translate);
		LuaCallback.RegisterFunc("Rotate", Rotate);
		LuaCallback.RegisterFunc("RotateAround", RotateAround);
		LuaCallback.RegisterFunc("LookAt", LookAt);
		LuaCallback.RegisterFunc("TransformDirection", TransformDirection);
		LuaCallback.RegisterFunc("InverseTransformDirection", InverseTransformDirection);
		LuaCallback.RegisterFunc("TransformVector", TransformVector);
		LuaCallback.RegisterFunc("InverseTransformVector", InverseTransformVector);
		LuaCallback.RegisterFunc("TransformPoint", TransformPoint);
		LuaCallback.RegisterFunc("InverseTransformPoint", InverseTransformPoint);
		LuaCallback.RegisterFunc("DetachChildren", DetachChildren);
		LuaCallback.RegisterFunc("SetAsFirstSibling", SetAsFirstSibling);
		LuaCallback.RegisterFunc("SetAsLastSibling", SetAsLastSibling);
		LuaCallback.RegisterFunc("SetSiblingIndex", SetSiblingIndex);
		LuaCallback.RegisterFunc("GetSiblingIndex", GetSiblingIndex);
		LuaCallback.RegisterFunc("Find", Find);
		LuaCallback.RegisterFunc("IsChildOf", IsChildOf);
		LuaCallback.RegisterFunc("GetEnumerator", GetEnumerator);
		LuaCallback.RegisterFunc("GetChild", GetChild);
		LuaCallback.RegisterVar("position", get_position, set_position);
		LuaCallback.RegisterVar("localPosition", get_localPosition, set_localPosition);
		LuaCallback.RegisterVar("eulerAngles", get_eulerAngles, set_eulerAngles);
		LuaCallback.RegisterVar("localEulerAngles", get_localEulerAngles, set_localEulerAngles);
		LuaCallback.RegisterVar("right", get_right, set_right);
		LuaCallback.RegisterVar("up", get_up, set_up);
		LuaCallback.RegisterVar("forward", get_forward, set_forward);
		LuaCallback.RegisterVar("rotation", get_rotation, set_rotation);
		LuaCallback.RegisterVar("localRotation", get_localRotation, set_localRotation);
		LuaCallback.RegisterVar("localScale", get_localScale, set_localScale);
		LuaCallback.RegisterVar("parent", get_parent, set_parent);
		LuaCallback.RegisterVar("worldToLocalMatrix", get_worldToLocalMatrix, null);
		LuaCallback.RegisterVar("localToWorldMatrix", get_localToWorldMatrix, null);
		LuaCallback.RegisterVar("root", get_root, null);
		LuaCallback.RegisterVar("childCount", get_childCount, null);
		LuaCallback.RegisterVar("lossyScale", get_lossyScale, null);
		LuaCallback.RegisterVar("hasChanged", get_hasChanged, set_hasChanged);
		LuaCallback.RegisterVar("hierarchyCapacity", get_hierarchyCapacity, set_hierarchyCapacity);
		LuaCallback.RegisterVar("hierarchyCount", get_hierarchyCount, null);
		LuaCallback.EndClass();
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int SetParent(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaCallback.ToObject(L, 2);
			arg0.SetParent(arg1);
			
			return 0;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2) && LuaAPI.IsBool(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaCallback.ToObject(L, 2);
			System.Boolean arg2 = (System.Boolean)LuaCallback.ToBool(L, 3);
			arg0.SetParent(arg1, arg2);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int SetPositionAndRotation(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2) && LuaAPI.IsVector4(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Quaternion arg2 = (UnityEngine.Quaternion)LuaCallback.ToQuaternion(L, 3);
			arg0.SetPositionAndRotation(arg1, arg2);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int Translate(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2) && LuaAPI.IsNumber(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Space arg2 = (UnityEngine.Space)LuaCallback.ToNumber(L, 3);
			arg0.Translate(arg1, arg2);
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			arg0.Translate(arg1);
			
			return 0;
		}
		if (nargs == 5 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4) && LuaAPI.IsNumber(L, 5))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			UnityEngine.Space arg4 = (UnityEngine.Space)LuaCallback.ToNumber(L, 5);
			arg0.Translate(arg1, arg2, arg3, arg4);
			
			return 0;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			arg0.Translate(arg1, arg2, arg3);
			
			return 0;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2) && LuaAPI.IsObject(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Transform arg2 = (UnityEngine.Transform)LuaCallback.ToObject(L, 3);
			arg0.Translate(arg1, arg2);
			
			return 0;
		}
		if (nargs == 5 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4) && LuaAPI.IsObject(L, 5))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			UnityEngine.Transform arg4 = (UnityEngine.Transform)LuaCallback.ToObject(L, 5);
			arg0.Translate(arg1, arg2, arg3, arg4);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int Rotate(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2) && LuaAPI.IsNumber(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Space arg2 = (UnityEngine.Space)LuaCallback.ToNumber(L, 3);
			arg0.Rotate(arg1, arg2);
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			arg0.Rotate(arg1);
			
			return 0;
		}
		if (nargs == 5 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4) && LuaAPI.IsNumber(L, 5))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			UnityEngine.Space arg4 = (UnityEngine.Space)LuaCallback.ToNumber(L, 5);
			arg0.Rotate(arg1, arg2, arg3, arg4);
			
			return 0;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			arg0.Rotate(arg1, arg2, arg3);
			
			return 0;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			UnityEngine.Space arg3 = (UnityEngine.Space)LuaCallback.ToNumber(L, 4);
			arg0.Rotate(arg1, arg2, arg3);
			
			return 0;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2) && LuaAPI.IsNumber(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			arg0.Rotate(arg1, arg2);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int RotateAround(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2) && LuaAPI.IsVector3(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Vector3 arg2 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			arg0.RotateAround(arg1, arg2, arg3);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int LookAt(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaCallback.ToObject(L, 2);
			UnityEngine.Vector3 arg2 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.LookAt(arg1, arg2);
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaCallback.ToObject(L, 2);
			arg0.LookAt(arg1);
			
			return 0;
		}
		if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Vector3 arg2 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.LookAt(arg1, arg2);
			
			return 0;
		}
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			arg0.LookAt(arg1);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int TransformDirection(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Vector3 res = arg0.TransformDirection(arg1);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			UnityEngine.Vector3 res = arg0.TransformDirection(arg1, arg2, arg3);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int InverseTransformDirection(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Vector3 res = arg0.InverseTransformDirection(arg1);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			UnityEngine.Vector3 res = arg0.InverseTransformDirection(arg1, arg2, arg3);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int TransformVector(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Vector3 res = arg0.TransformVector(arg1);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			UnityEngine.Vector3 res = arg0.TransformVector(arg1, arg2, arg3);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int InverseTransformVector(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Vector3 res = arg0.InverseTransformVector(arg1);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			UnityEngine.Vector3 res = arg0.InverseTransformVector(arg1, arg2, arg3);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int TransformPoint(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Vector3 res = arg0.TransformPoint(arg1);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			UnityEngine.Vector3 res = arg0.TransformPoint(arg1, arg2, arg3);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int InverseTransformPoint(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 2);
			UnityEngine.Vector3 res = arg0.InverseTransformPoint(arg1);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsNumber(L, 3) && LuaAPI.IsNumber(L, 4))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Single arg1 = (System.Single)LuaCallback.ToNumber(L, 2);
			System.Single arg2 = (System.Single)LuaCallback.ToNumber(L, 3);
			System.Single arg3 = (System.Single)LuaCallback.ToNumber(L, 4);
			UnityEngine.Vector3 res = arg0.InverseTransformPoint(arg1, arg2, arg3);
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int DetachChildren(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 1 && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			arg0.DetachChildren();
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int SetAsFirstSibling(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 1 && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			arg0.SetAsFirstSibling();
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int SetAsLastSibling(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 1 && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			arg0.SetAsLastSibling();
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int SetSiblingIndex(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Int32 arg1 = (System.Int32)LuaCallback.ToNumber(L, 2);
			arg0.SetSiblingIndex(arg1);
			
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int GetSiblingIndex(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 1 && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Int32 res = arg0.GetSiblingIndex();
			LuaCallback.PushNumber(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int Find(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
			UnityEngine.Transform res = arg0.Find(arg1);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int IsChildOf(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaCallback.ToObject(L, 2);
			System.Boolean res = arg0.IsChildOf(arg1);
			LuaCallback.PushBool(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int GetEnumerator(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 1 && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Collections.IEnumerator res = arg0.GetEnumerator();
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int GetChild(System.IntPtr L)
	{
		int nargs = LuaAPI.GetTop(L);
		if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Int32 arg1 = (System.Int32)LuaCallback.ToNumber(L, 2);
			UnityEngine.Transform res = arg0.GetChild(arg1);
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_position(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 res = arg0.position;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_localPosition(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 res = arg0.localPosition;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_eulerAngles(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 res = arg0.eulerAngles;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_localEulerAngles(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 res = arg0.localEulerAngles;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_right(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 res = arg0.right;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_up(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 res = arg0.up;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_forward(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 res = arg0.forward;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_rotation(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Quaternion res = arg0.rotation;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_localRotation(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Quaternion res = arg0.localRotation;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_localScale(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 res = arg0.localScale;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_parent(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Transform res = arg0.parent;
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_worldToLocalMatrix(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Matrix4x4 res = arg0.worldToLocalMatrix;
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_localToWorldMatrix(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Matrix4x4 res = arg0.localToWorldMatrix;
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_root(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Transform res = arg0.root;
			LuaCallback.PushObject(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_childCount(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Int32 res = arg0.childCount;
			LuaCallback.PushNumber(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_lossyScale(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 res = arg0.lossyScale;
			LuaCallback.PushVector(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_hasChanged(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Boolean res = arg0.hasChanged;
			LuaCallback.PushBool(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_hierarchyCapacity(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Int32 res = arg0.hierarchyCapacity;
			LuaCallback.PushNumber(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int get_hierarchyCount(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Int32 res = arg0.hierarchyCount;
			LuaCallback.PushNumber(L, res); 
			return 1;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_position(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.position = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_localPosition(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.localPosition = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_eulerAngles(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.eulerAngles = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_localEulerAngles(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.localEulerAngles = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_right(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.right = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_up(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.up = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_forward(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.forward = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_rotation(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector4(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Quaternion arg1 = (UnityEngine.Quaternion)LuaCallback.ToQuaternion(L, 3);
			arg0.rotation = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_localRotation(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector4(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Quaternion arg1 = (UnityEngine.Quaternion)LuaCallback.ToQuaternion(L, 3);
			arg0.localRotation = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_localScale(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsVector3(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaCallback.ToVector3(L, 3);
			arg0.localScale = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_parent(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsObject(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaCallback.ToObject(L, 3);
			arg0.parent = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_hasChanged(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsBool(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Boolean arg1 = (System.Boolean)LuaCallback.ToBool(L, 3);
			arg0.hasChanged = arg1;
			return 0;
		}
		return 0;
	}
	
	[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
	public static int set_hierarchyCapacity(System.IntPtr L)
	{
		if (true && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 3))
		{
			UnityEngine.Transform arg0 = (UnityEngine.Transform)LuaCallback.ToObject(L, 1);
			System.Int32 arg1 = (System.Int32)LuaCallback.ToNumber(L, 3);
			arg0.hierarchyCapacity = arg1;
			return 0;
		}
		return 0;
	}
}
