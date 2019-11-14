using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LuaTest : MonoBehaviour
{

    LuaEnv env = null;

    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.DoFile("test.lua");
#if HOTFIX
        env.DoFile("hotfix.lua");
#endif
    }

    // Update is called once per frame
    void OnDestroy()
    {
        env.Dispose();
    }
}