using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTranslator
{

    public static readonly ObjectTranslator Instance = new ObjectTranslator();

    List<object> objectPool = new List<object>();
    Dictionary<object, int> refCount = new Dictionary<object, int>();

    public int PushObj(object obj)
    {
        int idx = objectPool.IndexOf(obj);
        if (idx >= 0)
        {
            refCount[obj]++;
            return idx;
        }
        objectPool.Add(obj);
        refCount[obj] = 1;
        return objectPool.Count - 1;
    }

    public object Get(int index)
    {
        try
        {
            return objectPool[index];
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return null;
        }
    }

    public void PopObj(int index)
    {
        if (index < 0 || index >= objectPool.Count)
        {
            Debug.LogError("Pop Obj Error");
        }
        object obj = objectPool[index];
        refCount[obj]--;
        if (refCount[obj] == 0)
        {
            refCount.Remove(obj);
            objectPool.RemoveAt(index);
        }
    }
}