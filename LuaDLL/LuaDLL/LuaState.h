#pragma once

#include <string>
using namespace std;

#define DllImport  _declspec(dllimport)

extern "C"
{

#include "lua.h"
#include "lauxlib.h"
#include "lualib.h"

struct Object
{
	int obj;
};

DllImport lua_State* Init();
DllImport void DoString(lua_State* L, const char* s);
DllImport void AddResearchPath(lua_State* L, const char* filepath);
DllImport void DoFile(lua_State* L, const char* filename);
DllImport void Close(lua_State* L);

DllImport int GetTop(lua_State* L);
DllImport bool IsString(lua_State* L, int i);
DllImport const char* ToString(lua_State* L, int i);
DllImport void PushString(lua_State* L, const char* s);
DllImport bool IsNumber(lua_State* L, int i);
DllImport double ToNumber(lua_State* L, int i);
DllImport void PushNumber(lua_State* L, double d);
DllImport bool IsObject(lua_State* L, int idx);
DllImport int ToObject(lua_State* L, int idx);
DllImport void PushObject(lua_State* L, const char* classname, int obj);
DllImport void ToVector3(lua_State* L, int idx, float* x, float* y, float* z);
DllImport void PushVector3(lua_State* L, float x, float y, float z);
DllImport bool IsLuaFunction(lua_State* L, int idx);
DllImport int ToLuaFunction(lua_State* L);
DllImport void PushLuaFunction(lua_State* L, int ref);
DllImport void CallLuaFunction(lua_State* L, int nargs, int nresults);

DllImport void RegisterLuaFunc(lua_State* L, const char* funcname, lua_CFunction funcptr);

int RegisterIndex(lua_State* L);
int RegisterNewIndex(lua_State* L);

DllImport void BeginClass(lua_State* L, const char* classname, const char* baseclassname);
DllImport void RegisterFunc(lua_State* L, const char* funcname, lua_CFunction funcptr);
DllImport void RegisterVar(lua_State* L, const char* varname, lua_CFunction getptr, lua_CFunction setptr);
DllImport void EndClass(lua_State* L);

}