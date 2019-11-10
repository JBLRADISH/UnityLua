using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotFix : MonoBehaviour
{

    public static DelegateHelper addHotFix = null;
    int Add(int a, int b)
    {
        if (addHotFix != null)
        {
            return addHotFix.Invoke(this, a, b);
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