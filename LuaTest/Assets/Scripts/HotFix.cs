using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotFix : MonoBehaviour
{

    [HotFix]
    public int Add(int a, int b)
    {
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