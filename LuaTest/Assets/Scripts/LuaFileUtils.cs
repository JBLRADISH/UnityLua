using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LuaFileUtils
{
    public static readonly LuaFileUtils Instance = new LuaFileUtils();

    List<string> researchPathList = new List<string>();

    public void AddResearchPath(string filepath)
    {
        researchPathList.Add(filepath);
    }

    public string GetFilePath(string filepath)
    {
        if (Path.IsPathRooted(filepath))
        {
            if (!filepath.EndsWith(".lua"))
            {
                filepath += ".lua";
            }
            if (File.Exists(filepath))
            {
                return filepath;
            }
            else
            {
                return null;
            }
        }
        if (filepath.EndsWith(".lua"))
        {
            filepath = filepath.Substring(0, filepath.Length - 4);
        }
        string realFilePath = null;
        for (int i = 0; i < researchPathList.Count; i++)
        {
            realFilePath = researchPathList[i].Replace("?", filepath);
            if (File.Exists(realFilePath))
            {
                return realFilePath;
            }
        }
        return null;
    }
}