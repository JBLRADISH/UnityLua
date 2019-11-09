#include "LuaState.h"

lua_State* Init()
{
	lua_State* L = luaL_newstate();
	luaL_openlibs(L);
	return L;
}

void DoString(lua_State* L, const char* s)
{
	luaL_dostring(L, s);
}

void AddResearchPath(lua_State* L, const char* filepath)
{
	lua_getglobal(L, "package");
	lua_getfield(L, -1, "path");
	string v;
	v.append(lua_tostring(L, -1));
	v.append(";");
	v.append(filepath);
	lua_pushstring(L, v.c_str());
	lua_setfield(L, -3, "path");
	lua_pop(L, 2);
}

void DoFile(lua_State* L, const char* filename)
{
	luaL_dofile(L, filename);
}

void Close(lua_State* L)
{
	lua_close(L);
}

int GetTop(lua_State* L)
{
	return lua_gettop(L);
}

bool IsString(lua_State* L, int i)
{
	return lua_isstring(L, i);
}

const char* ToString(lua_State* L, int i)
{
	return lua_tostring(L, i);
}

void PushString(lua_State* L, const char* s)
{
	lua_pushstring(L, s);
}

bool IsNumber(lua_State* L, int i)
{
	return lua_isnumber(L, i);
}

double ToNumber(lua_State* L, int i)
{
	return lua_tonumber(L, i);
}

void PushNumber(lua_State* L, double d)
{
	lua_pushnumber(L, d);
}

bool IsBool(lua_State* L, int i)
{
	return lua_isboolean(L, i);
}

bool ToBool(lua_State* L, int i)
{
	return lua_toboolean(L, i);
}

void PushBool(lua_State* L, bool b)
{
	lua_pushboolean(L, b);
}

bool IsObject(lua_State* L, int idx)
{
	return lua_isuserdata(L, idx);
}

int ToObject(lua_State* L, int idx)
{
	return ((Object*)lua_touserdata(L, idx))->obj;
}

void PushObject(lua_State* L, const char* classname, int obj)
{
	Object* object = (Object*)lua_newuserdata(L, sizeof(Object));
	object->obj = obj;
	lua_getglobal(L, classname);
	lua_getmetatable(L, -1);
	lua_setmetatable(L, -3);
	lua_pop(L, 1);
}

bool IsVector2(lua_State* L, int idx)
{
	return KVTableNumber(L, idx, "x") && KVTableNumber(L, idx, "y");
}

void ToVector2(lua_State* L, int idx, float* x, float* y)
{
	lua_getfield(L, idx, "x");
	*x = lua_tonumber(L, -1);
	lua_pop(L, 1);
	lua_getfield(L, idx, "y");
	*y = lua_tonumber(L, -1);
	lua_pop(L, 1);
}

void PushVector2(lua_State* L, float x, float y)
{
	lua_newtable(L);
	lua_pushstring(L, "x");
	lua_pushnumber(L, x);
	lua_rawset(L, -3);
	lua_pushstring(L, "y");
	lua_pushnumber(L, y);
	lua_rawset(L, -3);
}

bool IsVector3(lua_State* L, int idx)
{
	return IsVector2(L, idx) && KVTableNumber(L, idx, "z");
}

void ToVector3(lua_State* L, int idx, float* x, float* y, float* z)
{
	ToVector2(L, idx, x, y);
	lua_getfield(L, idx, "z");
	*z = lua_tonumber(L, -1);
	lua_pop(L, 1);
}

void PushVector3(lua_State* L, float x, float y, float z)
{
	PushVector2(L, x, y);
	lua_pushstring(L, "z");
	lua_pushnumber(L, z);
	lua_rawset(L, -3);
}

bool IsVector4(lua_State* L, int idx)
{
	return IsVector3(L, idx) && KVTableNumber(L, idx, "w");
}

void ToVector4(lua_State* L, int idx, float* x, float* y, float* z, float* w)
{
	ToVector3(L, idx, x, y, z);
	lua_getfield(L, idx, "w");
	*w = lua_tonumber(L, -1);
	lua_pop(L, 1);
}

void PushVector4(lua_State* L, float x, float y, float z, float w)
{
	PushVector3(L, x, y, z);
	lua_pushstring(L, "w");
	lua_pushnumber(L, w);
	lua_rawset(L, -3);
}

bool IsLuaFunction(lua_State* L, int idx)
{
	return lua_isfunction(L, idx);
}

int ToLuaFunction(lua_State* L)
{
	return luaL_ref(L, LUA_REGISTRYINDEX);
}

void PushLuaFunction(lua_State* L, int ref)
{
	lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
}

void CallLuaFunction(lua_State* L, int nargs, int nresults)
{
	lua_call(L, nargs, nresults);
}

void RawSetI(lua_State* L, int idx, int i)
{
	lua_rawseti(L, idx, i);
}

void NewTable(lua_State* L)
{
	lua_newtable(L);
}

unordered_map<string, lua_CFunction> funcmap;

void RegisterLuaFunc(lua_State* L, const char* funcname, lua_CFunction funcptr)
{
	lua_register(L, funcname, funcptr);
	funcmap[funcname] = funcptr;
}

void BeginClass(lua_State* L, const char* classname, const char* baseclassname, int idx)
{
	lua_newtable(L);
	lua_setglobal(L, classname);
	lua_getglobal(L, classname);
	string c = classname;
	luaL_newmetatable(L, (c + "_meta").c_str());
	lua_pushstring(L, "T");
	lua_pushnumber(L, idx);
	lua_rawset(L, -3);
	lua_pushstring(L, "__index");
	lua_pushcfunction(L, RegisterIndex);
	lua_rawset(L, -3);
	lua_pushstring(L, "__newindex");
	lua_pushcfunction(L, RegisterNewIndex);
	lua_rawset(L, -3);
	lua_pushstring(L, "__gc");
	lua_pushcfunction(L, RegisterGC);
	lua_rawset(L, -3);
	if (baseclassname)
	{
		lua_getglobal(L, baseclassname);
		lua_getmetatable(L, -1);
		lua_setmetatable(L, -3);
		lua_pop(L, 1);
	}
	lua_setmetatable(L, -2);
	lua_getmetatable(L, -1);
}

void RegisterFunc(lua_State* L, const char* funcname, lua_CFunction funcptr)
{
	string func = funcname;
	lua_pushstring(L, ("get_" + func).c_str());
	lua_newtable(L);
	lua_pushstring(L, "func");
	lua_pushcfunction(L, funcptr);
	lua_rawset(L, -3);
	lua_rawset(L, -3);
}

bool KVTable(lua_State* L, const char* key)
{
	int nIndex = lua_gettop(L);
	lua_pushnil(L);
	while (0 != lua_next(L, nIndex))
	{
		lua_pop(L, 1);
		if (strcmp(lua_tostring(L, -1), key) == 0)
		{
			lua_pop(L, 1);
			return true;
		}
	}
	return false;
}

bool KVTableNumber(lua_State* L, int idx, const char* key)
{
	lua_pushnil(L);
	while (0 != lua_next(L, idx))
	{
		if (lua_isnumber(L, -1))
		{
			lua_pop(L, 1);
			if (strcmp(lua_tostring(L, -1), key) == 0)
			{
				lua_pop(L, 1);
				return true;
			}
		}
		else
		{
			lua_pop(L, 1);
		}
	}
	return false;
}

int RegisterIndex(lua_State* L)
{
	const char* membername = lua_tostring(L, -1);
	char getmembername[64] = { 0 };
	strcat_s(getmembername, "get_");
	strcat_s(getmembername, membername);
	int idx = lua_getmetatable(L, -2);
	while (idx != 0 && !KVTable(L, getmembername))
	{
		idx = lua_getmetatable(L, -1);
	}
	if (idx == 0)
	{
		return 0;
	}
	lua_pushstring(L, getmembername);
	lua_rawget(L, -2);
	if (lua_istable(L, -1))
	{
		lua_getfield(L, -1, "func");
		return 1;
	}
	else if (lua_iscfunction(L, -1))
	{
		lua_CFunction getptr = lua_tocfunction(L, -1);
		lua_pop(L, lua_gettop(L) - 2);
		return getptr(L);
	}
	else
	{
		return 0;
	}
}

int RegisterGC(lua_State* L)
{
	return funcmap["gc"](L);
}

int RegisterNewIndex(lua_State* L)
{
	const char* varname = lua_tostring(L, -2);
	char setvarname[64] = { 0 };
	strcat_s(setvarname, "set_");
	strcat_s(setvarname, varname);
	int idx = lua_getmetatable(L, -3);
	while (idx != 0 && !KVTable(L, setvarname))
	{
		idx = lua_getmetatable(L, -1);
	}
	if (idx == 0)
	{
		return 0;
	}
	lua_pushstring(L, setvarname);
	lua_rawget(L, -2);
	if (lua_iscfunction(L, -1))
	{
		lua_CFunction setptr = lua_tocfunction(L, -1);
		lua_pop(L, lua_gettop(L) - 3);
		return setptr(L);
	}
	return 0;
}

void RegisterVar(lua_State* L, const char* varname, lua_CFunction getptr, lua_CFunction setptr)
{
	string var = varname;
	if (getptr)
	{
		lua_pushstring(L, ("get_" + var).c_str());
		lua_pushcfunction(L, getptr);
		lua_rawset(L, -3);
	}
	if (setptr)
	{
		lua_pushstring(L, ("set_" + var).c_str());
		lua_pushcfunction(L, setptr);
		lua_rawset(L, -3);
	}
}

void EndClass(lua_State* L)
{
	lua_pop(L, 2);
}