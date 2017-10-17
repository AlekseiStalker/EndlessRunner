using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData
{
    public static Container stuffContainer = new Container();

    public delegate void SerializeAction();
    public static event SerializeAction OnLoaded;
    public static event SerializeAction OnBeforeSave;

    public static void Load(string dataPath)
    {
        stuffContainer = LoadStuff(dataPath);

        foreach (StuffData data in stuffContainer.stuff)
        {
           GameSaveLoad.CreateStuff(data, GameSaveLoad.stuffPath[data.name], data.pos, Quaternion.identity);
        }

        OnLoaded();
        ClearStuffList();
    }

    public static void Save(string path, Container stuffs)
    { 
        OnBeforeSave();

        SaveStuff(path, stuffs);

        ClearStuffList();
    }

    public static void AddStuffData(StuffData data)
    {
        stuffContainer.stuff.Add(data);
    }
    public static void ClearStuffList()
    {
        stuffContainer.stuff.Clear();
    }

    static Container LoadStuff(string path)
    {
        string jsonString = File.ReadAllText(path);

        return JsonUtility.FromJson<Container>(jsonString);
    }

    static void SaveStuff(string path, Container stuffs)
    {
        string jsonString = JsonUtility.ToJson(stuffs);

        StreamWriter sw = File.CreateText(path);//Попробовать без переменной
        sw.Close();

        File.WriteAllText(path, jsonString);
    }
     
}
