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

void ToVector3(lua_State* L, int idx, float* x, float* y, float* z)
{
	lua_getfield(L, idx, "x");
	if (lua_isnumber(L, -1))
	{
		*x = lua_tonumber(L, -1);
	}
	lua_pop(L, 1);
	lua_getfield(L, idx, "y");
	if (lua_isnumber(L, -1))
	{
		*y = lua_tonumber(L, -1);
	}
	lua_pop(L, 1);
	lua_getfield(L, idx, "z");
	if (lua_isnumber(L, -1))
	{
		*z = lua_tonumber(L, -1);
	}
	lua_pop(L, 1);
}

void PushVector3(lua_State* L, float x, float y, float z)
{
	lua_newtable(L);
	lua_pushstring(L, "x");
	lua_pushnumber(L, x);
	lua_rawset(L, -3);
	lua_pushstring(L, "y");
	lua_pushnumber(L, y);
	lua_rawset(L, -3);
	lua_pushstring(L, "z");
	lua_pushnumber(L, z);
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

void RegisterLuaFunc(lua_State* L, const char* funcname, lua_CFunction funcptr)
{
	lua_register(L, funcname, funcptr);
}

void BeginClass(lua_State* L, const char* classname, const char* baseclassname)
{
	lua_newtable(L);
	lua_setglobal(L, classname);
	lua_getglobal(L, classname);
	luaL_newmetatable(L, "meta");
	lua_pushstring(L, "__index");
	lua_pushcfunction(L, RegisterIndex);
	lua_rawset(L, -3);
	lua_pushstring(L, "__newindex");
	lua_pushcfunction(L, RegisterNewIndex);
	lua_rawset(L, -3);
	if (baseclassname)
	{
		lua_getglobal(L, baseclassname);
		lua_setmetatable(L, -2);
	}
	lua_setmetatable(L, -2);
}

void RegisterFunc(lua_State* L, const char* funcname, lua_CFunction funcptr)
{
	lua_pushstring(L, funcname);
	lua_pushcfunction(L, funcptr);
	lua_rawset(L, -3);
}

int RegisterIndex(lua_State* L)
{
	const char* varname = lua_tostring(L, -1);
	string var = varname;
	lua_getmetatable(L, -2);
	lua_getfield(L, -1, ("get_" + var).c_str());
	lua_CFunction getptr = lua_tocfunction(L, -1);
	lua_pop(L, 2);
	return getptr(L);
}

int RegisterNewIndex(lua_State* L)
{
	const char* varname = lua_tostring(L, -2);
	string var = varname;
	lua_getmetatable(L, -3);
	lua_getfield(L, -1, ("set_" + var).c_str());
	lua_CFunction setptr = lua_tocfunction(L, -1);
	lua_pop(L, 2);
	return setptr(L);
}

void RegisterVar(lua_State* L, const char* varname, lua_CFunction getptr, lua_CFunction setptr)
{
	lua_getmetatable(L, -1);
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
	lua_pop(L, 1);
}

void EndClass(lua_State* L)
{
	lua_pop(L, 1);
}