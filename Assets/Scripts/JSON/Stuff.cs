using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuff : MonoBehaviour
{
    public StuffData data = new StuffData();

    public string namePrefab; 

    public void StoreData()
    {
        data.name = namePrefab;
        data.pos = transform.position;
    }

    public void LoadData()
    {
        namePrefab = data.name;
        transform.position = data.pos;
    }

    public void ApplyData()
    {
        SaveData.AddStuffData(data);
    }

    private void OnEnable()
    {
        SaveData.OnLoaded += LoadData;
        SaveData.OnBeforeSave += StoreData;
        SaveData.OnBeforeSave += ApplyData;
    }

    private void OnDisable()
    {
        SaveData.OnLoaded -= LoadData;
        SaveData.OnBeforeSave -= StoreData;
        SaveData.OnBeforeSave -= ApplyData;
    }
}

[System.Serializable]
public class StuffData
{
    public string name;
    public Vector3 pos;
}
