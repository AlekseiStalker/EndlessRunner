using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonControl : MonoBehaviour
{
    public float speed = 3;
    public float increaseSpeed;
    public float speedCounter;
    float tempSpeedCounter;
    public PlayerControll playerControl;
    public const float START_BEHIND_PLAYER = 7;
    public Text leaveDragon;

    Rigidbody2D _rb;

    private void Start()
    {
        transform.position = new Vector3(playerControl.transform.position.x - START_BEHIND_PLAYER, 0, 0);
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (speedCounter > (tempSpeedCounter + 2))
        {
            speed += increaseSpeed;
            tempSpeedCounter = speedCounter;
        }
        speedCounter += Time.fixedDeltaTime;

        _rb.velocity = new Vector2(speed, _rb.velocity.y); 
        
        float positionY = Mathf.Clamp(playerControl.transform.position.y, -2, 2);
        transform.position = new Vector3(transform.position.x, positionY, 0);

        if (Input.GetMouseButtonDown(2) && GameManager.instance.coint >= 1)
        {
            StartCoroutine(LeaveGragonText());
            GameManager.instance.UpdateCointText(--GameManager.instance.coint);
            transform.position = new Vector3(transform.position.x - START_BEHIND_PLAYER, positionY, 0);
        }
    } 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            print("Тебя сожрал дракон.");
            GameManager.instance.Restart();
        }
    }
    IEnumerator LeaveGragonText()
    {
        leaveDragon.text = "Get away from me!";
        leaveDragon.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f); 
        leaveDragon.gameObject.SetActive(false);
    }
}
