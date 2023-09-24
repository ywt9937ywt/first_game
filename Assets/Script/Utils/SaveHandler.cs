using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using System.IO;

public static class SaveHandler
{

    private static string GetPath(string  filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }
    public static void SaveToJSON<T>(List<T> toSave, string filename)
    {
        string content = JsonHelper.ToJson<T>(toSave.ToArray(), true);
        WriteJSONFile(GetPath(filename), content);
    }

    public static List<T> ReadFromJSON<T>(string filename)
    {
        string content = ReadJSONFile(GetPath(filename));
        if(string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
        }
        List<T> res = JsonHelper.FromJson<T>(content).ToList();
        return res;
    }

    private static void WriteJSONFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using(StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    private static string ReadJSONFile(string path)
    {
        if (File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }

    /*public static bool SaveToBinary(string saveName, object saveData)
    {

    }*/

}

public static class JsonHelper
{
    public static T[] FromJson<T> (string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T> (T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
