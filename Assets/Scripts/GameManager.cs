using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    [Header("UI")]
    public Text CointsText; 
    public int coint = 0;
    public Text ScoreTimeText;
    public Text BestScoreText; 
    float timeStart; 
    float scoreTime;
    [Space]
    [Header("GameObjects")]
    public Transform platformGenerator; 
    public PlayerControll thePlayer;
    public DragonControl theDragon;

    Vector3 platformStartPos;
    Vector3 playerStartPos;
    Vector3 dragonStartPos;
    float dragonSpeed;

    PlatformDestroyer[] platfromList;
     
    private void Awake()
    { 
        if (instance == null)
        {
            instance = this;
        } 
    }

    void Start()
    { 
        platformStartPos = platformGenerator.position;
        playerStartPos = thePlayer.transform.position;
        dragonStartPos = theDragon.transform.position;
        dragonSpeed = theDragon.speed;
         
        timeStart = Time.time;
        scoreTime = Time.time - timeStart;
        ScoreTimeText.text = "Score: " + scoreTime.ToString();
        BestScoreText.text = "Best_score: " + PlayerPrefs.GetFloat("BestScore", 0);
    }

    private void Update()
    {
        scoreTime = (int)Time.time - timeStart;
        ScoreTimeText.text = "Score: " + scoreTime.ToString();
    }

    public void Restart()
    {
        if (scoreTime > PlayerPrefs.GetFloat("BestScore", 0))
        {
            PlayerPrefs.SetFloat("BestScore", scoreTime);
        }
        StartCoroutine("RestartGame");

        UpdateCointText(0); 
    }

    public IEnumerator RestartGame()
    {
        thePlayer.gameObject.SetActive(false);
        theDragon.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.3f);

        platfromList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platfromList.Length; i++)
        {
            Destroy(platfromList[i].gameObject);
        }

        thePlayer.transform.position = playerStartPos;
        platformGenerator.position = platformStartPos;
        theDragon.transform.position = dragonStartPos;
        theDragon.speed = dragonSpeed;

        theDragon.gameObject.SetActive(true);
        thePlayer.gameObject.SetActive(true);
    }

    public void UpdateCointText(int coint)
    {
        CointsText.text = "Coints: " + coint.ToString();
    }
    public void OnExitClick()
    { 
        SceneManager.LoadScene(0);
    }

}
