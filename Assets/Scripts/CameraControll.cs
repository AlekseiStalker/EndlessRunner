using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    GameObject playerTarget;

    const float PLAYER_START_posX = 2;
    Vector3 lastPlayerPos;
    float distanceToMove;

	void Start ()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        lastPlayerPos = playerTarget.transform.position;

        transform.position = new Vector3(playerTarget.transform.position.x + PLAYER_START_posX, transform.position.y, transform.position.z);
    }
    
	void Update ()
    {
        distanceToMove = playerTarget.transform.position.x - lastPlayerPos.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

        lastPlayerPos = playerTarget.transform.position;
	}
}
