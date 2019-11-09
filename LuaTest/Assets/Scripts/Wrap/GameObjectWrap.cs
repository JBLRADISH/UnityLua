public static class GameObjectWrap
{
public static void Register()
{
LuaCallback.BeginClass(typeof(UnityEngine.GameObject), typeof(UnityEngine.Object));
LuaCallback.RegisterFunc("CreatePrimitive", CreatePrimitive);
LuaCallback.RegisterFunc("GetComponent", GetComponent);
LuaCallback.RegisterFunc("GetComponentInChildren", GetComponentInChildren);
LuaCallback.RegisterFunc("GetComponentInParent", GetComponentInParent);
LuaCallback.RegisterFunc("GetComponents", GetComponents);
LuaCallback.RegisterFunc("GetComponentsInChildren", GetComponentsInChildren);
LuaCallback.RegisterFunc("GetComponentsInParent", GetComponentsInParent);
LuaCallback.RegisterFunc("FindWithTag", FindWithTag);
LuaCallback.RegisterFunc("SendMessageUpwards", SendMessageUpwards);
LuaCallback.RegisterFunc("SendMessage", SendMessage);
LuaCallback.RegisterFunc("BroadcastMessage", BroadcastMessage);
LuaCallback.RegisterFunc("AddComponent", AddComponent);
LuaCallback.RegisterFunc("SetActive", SetActive);
LuaCallback.RegisterFunc("CompareTag", CompareTag);
LuaCallback.RegisterFunc("FindGameObjectWithTag", FindGameObjectWithTag);
LuaCallback.RegisterFunc("FindGameObjectsWithTag", FindGameObjectsWithTag);
LuaCallback.RegisterFunc("Find", Find);
LuaCallback.EndClass();
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int CreatePrimitive(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 1 && LuaAPI.IsNumber(L, 1))
{

UnityEngine.PrimitiveType arg0 = (UnityEngine.PrimitiveType)LuaCallback.ToNumber(L, 1);
UnityEngine.GameObject res = UnityEngine.GameObject.CreatePrimitive(arg0);
LuaCallback.PushObject(L, res); 
return 1;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int GetComponent(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
UnityEngine.Component res = arg0.GetComponent(arg1);
LuaCallback.PushObject(L, res); 
return 1;
}
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
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
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
System.Boolean arg2 = (System.Boolean)LuaCallback.ToBool(L, 3);
UnityEngine.Component res = arg0.GetComponentInChildren(arg1, arg2);
LuaCallback.PushObject(L, res); 
return 1;
}
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
UnityEngine.Component res = arg0.GetComponentInChildren(arg1);
LuaCallback.PushObject(L, res); 
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
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
UnityEngine.Component res = arg0.GetComponentInParent(arg1);
LuaCallback.PushObject(L, res); 
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
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
UnityEngine.Component[] res = arg0.GetComponents(arg1);
LuaCallback.PushArray(L, res); 
return 1;
}
if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsObject(L, 3))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
System.Collections.Generic.List<UnityEngine.Component> arg2 = (System.Collections.Generic.List<UnityEngine.Component>)LuaCallback.ToObject(L, 3);
arg0.GetComponents(arg1, arg2);

return 0;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int GetComponentsInChildren(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
UnityEngine.Component[] res = arg0.GetComponentsInChildren(arg1);
LuaCallback.PushArray(L, res); 
return 1;
}
if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsBool(L, 3))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
System.Boolean arg2 = (System.Boolean)LuaCallback.ToBool(L, 3);
UnityEngine.Component[] res = arg0.GetComponentsInChildren(arg1, arg2);
LuaCallback.PushArray(L, res); 
return 1;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int GetComponentsInParent(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
UnityEngine.Component[] res = arg0.GetComponentsInParent(arg1);
LuaCallback.PushArray(L, res); 
return 1;
}
if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2) && LuaAPI.IsBool(L, 3))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
System.Boolean arg2 = (System.Boolean)LuaCallback.ToBool(L, 3);
UnityEngine.Component[] res = arg0.GetComponentsInParent(arg1, arg2);
LuaCallback.PushArray(L, res); 
return 1;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int FindWithTag(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 1 && LuaAPI.IsString(L, 1))
{

System.String arg0 = (System.String)LuaCallback.ToString(L, 1);
UnityEngine.GameObject res = UnityEngine.GameObject.FindWithTag(arg0);
LuaCallback.PushObject(L, res); 
return 1;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int SendMessageUpwards(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsNumber(L, 3))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 3);
arg0.SendMessageUpwards(arg1, arg2);

return 0;
}
if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3) && LuaAPI.IsNumber(L, 4))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 4);
arg0.SendMessageUpwards(arg1, arg2, arg3);

return 0;
}
if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
arg0.SendMessageUpwards(arg1, arg2);

return 0;
}
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
arg0.SendMessageUpwards(arg1);

return 0;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int SendMessage(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsNumber(L, 3))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 3);
arg0.SendMessage(arg1, arg2);

return 0;
}
if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3) && LuaAPI.IsNumber(L, 4))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 4);
arg0.SendMessage(arg1, arg2, arg3);

return 0;
}
if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
arg0.SendMessage(arg1, arg2);

return 0;
}
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
arg0.SendMessage(arg1);

return 0;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int BroadcastMessage(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsNumber(L, 3))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 3);
arg0.BroadcastMessage(arg1, arg2);

return 0;
}
if (nargs == 4 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3) && LuaAPI.IsNumber(L, 4))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)LuaCallback.ToNumber(L, 4);
arg0.BroadcastMessage(arg1, arg2, arg3);

return 0;
}
if (nargs == 3 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2) && LuaAPI.IsObject(L, 3))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
System.Object arg2 = (System.Object)LuaCallback.ToObject(L, 3);
arg0.BroadcastMessage(arg1, arg2);

return 0;
}
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsString(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
arg0.BroadcastMessage(arg1);

return 0;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int AddComponent(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsNumber(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Type arg1 = (System.Type)LuaCallback.ToType(L, 2);
UnityEngine.Component res = arg0.AddComponent(arg1);
LuaCallback.PushObject(L, res); 
return 1;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int SetActive(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 2 && LuaAPI.IsObject(L, 1) && LuaAPI.IsBool(L, 2))
{
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.Boolean arg1 = (System.Boolean)LuaCallback.ToBool(L, 2);
arg0.SetActive(arg1);

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
UnityEngine.GameObject arg0 = (UnityEngine.GameObject)LuaCallback.ToObject(L, 1);
System.String arg1 = (System.String)LuaCallback.ToString(L, 2);
System.Boolean res = arg0.CompareTag(arg1);
LuaCallback.PushBool(L, res); 
return 1;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int FindGameObjectWithTag(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 1 && LuaAPI.IsString(L, 1))
{

System.String arg0 = (System.String)LuaCallback.ToString(L, 1);
UnityEngine.GameObject res = UnityEngine.GameObject.FindGameObjectWithTag(arg0);
LuaCallback.PushObject(L, res); 
return 1;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int FindGameObjectsWithTag(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 1 && LuaAPI.IsString(L, 1))
{

System.String arg0 = (System.String)LuaCallback.ToString(L, 1);
UnityEngine.GameObject[] res = UnityEngine.GameObject.FindGameObjectsWithTag(arg0);
LuaCallback.PushArray(L, res); 
return 1;
}
return 0;
}

[AOT.MonoPInvokeCallback(typeof(LuaCallback.LuaCFunction))]
public static int Find(System.IntPtr L)
{
int nargs = LuaAPI.GetTop(L);
if (nargs == 1 && LuaAPI.IsString(L, 1))
{

System.String arg0 = (System.String)LuaCallback.ToString(L, 1);
UnityEngine.GameObject res = UnityEngine.GameObject.Find(arg0);
LuaCallback.PushObject(L, res); 
return 1;
}
return 0;
}
}
