using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveLoad : MonoBehaviour
{
    public static GameSaveLoad instance;

    public PlatformGenerator platformGen;

    public GameObject Player;
    public GameObject Dragon;
    public GameObject[] stuffPrefabs;

    public static IDictionary<string, string> stuffPath = new Dictionary<string, string>();
    public static string dataPath;

    private void Awake()
    {
        dataPath = System.IO.Path.Combine(Application.dataPath, "stuff.json");

        stuffPath.Add("Box1", "Prefabs/Box1");
        stuffPath.Add("Box2", "Prefabs/Box2");
        stuffPath.Add("Box3", "Prefabs/Box3");
        stuffPath.Add("Platform3x1", "Prefabs/Platform3x1");
        stuffPath.Add("Platform4x1", "Prefabs/Platform4x1");
        stuffPath.Add("Platform5x1", "Prefabs/Platform5x1");
        stuffPath.Add("Platform6x1", "Prefabs/Platform6x1");
        stuffPath.Add("Platform7x1", "Prefabs/Platform7x1");

        if (instance == null)
        {
            instance = this;
        }

        platformGen = platformGen.GetComponent<PlatformGenerator>();
    }

    public static Stuff CreateStuff(string path, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        GameObject go = Instantiate(prefab, position, rotation) as GameObject;

        Stuff stuff = go.GetComponent<Stuff>() ?? go.AddComponent<Stuff>();

        return stuff;
    }

    public static Stuff CreateStuff(StuffData data, string path, Vector3 position, Quaternion rotation)
    {
        Stuff stuff = CreateStuff(path, position, rotation);

        stuff.data = data;

        return stuff;
    }

    public void SaveScene()
    {
        Save_PlayerPosition();
        Save_DragonPosition();
        SaveData.Save(dataPath, SaveData.stuffContainer);
    }

    public void LoadScene()
    {
        StartCoroutine("DeactiveGenerate");
        Load_PlayerPositon();
        Load_DragonPositon();
        SaveData.Load(dataPath);
    }

    void Save_DragonPosition()
    {
        PlayerPrefs.SetFloat("dragonPosX", Dragon.transform.position.x);
        PlayerPrefs.SetFloat("dragonPosY", Dragon.transform.position.y);
    }

    void Save_PlayerPosition()
    {
        PlayerPrefs.SetFloat("posX", Player.transform.position.x);
        PlayerPrefs.SetFloat("posY", Player.transform.position.y);
    }
    void Load_PlayerPositon()
    {
        Player.transform.position = new Vector3(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"), 0);
    }
    void Load_DragonPositon()
    {
        Dragon.transform.position = new Vector3(PlayerPrefs.GetFloat("dragonPosX"), PlayerPrefs.GetFloat("dragonPosY"), 0);
    }
    
    IEnumerator DeactiveGenerate()
    {
        platformGen.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.1f);
        platformGen.gameObject.SetActive(true);
    }
}
