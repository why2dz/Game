using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonMgr 
{
    private static JsonMgr  instance = new JsonMgr();

    public static JsonMgr  Instance { get { return instance; } }

    private JsonMgr() { }

    public void Savedate(object data, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".Json";
        string JsonDate = "";
        JsonDate = JsonUtility.ToJson(data);

        File.WriteAllText(path, JsonDate);
    }

    public T LoadData<T>(string fileName) where T : new()
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".Json";
        if(!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".Json";
        if (!File.Exists(path))
            return new T();

        string JsonDate = File.ReadAllText(path);
        T Data = default(T);
        Data = JsonUtility.FromJson<T>(JsonDate);
        return Data;
    }

}
