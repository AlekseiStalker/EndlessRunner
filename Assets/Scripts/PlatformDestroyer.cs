using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    GameObject pointDestroyPlatform;

    private void Start()
    {
        pointDestroyPlatform = GameObject.FindGameObjectWithTag("PlatformDestroyPoint");
    }

    void Update ()
    {
        if (transform.position.x < pointDestroyPlatform.transform.position.x)
        {
            Destroy(gameObject); 
        }
	}
}
