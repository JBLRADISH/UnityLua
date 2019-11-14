using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotFixDebug : MonoBehaviour
{

    public static DelegateHelperDebug addHotFix = null;

    int Add(int a, int b)
    {
        if (addHotFix != null)
        {
            return addHotFix.Invoke(a, b);
        }
        return a + b;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(Add(1, 2));
        }
    }
}