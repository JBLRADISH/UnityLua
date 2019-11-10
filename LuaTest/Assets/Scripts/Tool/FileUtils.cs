using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileUtils
{
    public static void FileFormatter(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.Log(filePath + " not exist!");
        }
        string prefix = "";
        string[] contents = File.ReadAllLines(filePath);
        for (int i = 0; i < contents.Length; i++)
        {
            if (contents[i].StartsWith("{"))
            {
                contents[i] = prefix + contents[i];
                prefix += '\t';
            }
            else if (contents[i].StartsWith("}"))
            {
                prefix = prefix.Substring(1);
                contents[i] = prefix + contents[i];
            }
            else
            {
                contents[i] = prefix + contents[i];
            }
        }
        File.WriteAllLines(filePath, contents);
    }
}
