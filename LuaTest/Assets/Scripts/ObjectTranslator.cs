using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTranslator
{

    public static readonly ObjectTranslator Instance = new ObjectTranslator();

    List<UnityEngine.Object> objectPool = new List<UnityEngine.Object>();

    public int PushObj(UnityEngine.Object obj)
    {
        int idx = objectPool.IndexOf(obj);
        if (idx >= 0)
        {
            return idx;
        }
        objectPool.Add(obj);
        return objectPool.Count - 1;
    }

    public T Get<T>(int index) where T : UnityEngine.Object
    {
        try
        {
            return objectPool[index] as T;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return null;
        }
    }
}