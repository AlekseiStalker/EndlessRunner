using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel.Design.Serialization;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] thePlatform;
    public Transform pointGaneratePlatform;
    public GameObject theCoins;
    public GameObject[] Boxes;

    public float distanceBetweenMin = 1;
    public float distanceBetweenMax = 3; 
    float distanceBetween; 
    float[] platformWidth;
    Transform platrfomsHolder;
    Transform cointBoxHolder;

    public Transform maxHeightPoint;
    public float maxHeightChange;
    float minHeight;
    float maxHeight;
    float heightChange;

    void Start ()
    { 
        platrfomsHolder = new GameObject("PlatformHolder").transform;
        cointBoxHolder = new GameObject("CointBoxHolder").transform;

        platformWidth = new float[thePlatform.Length];
        for (int i = 0; i < thePlatform.Length; i++)
        {
            platformWidth[i] = thePlatform[i].GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
	}
	 
	void Update ()
    {
        if (cointBoxHolder == null && platrfomsHolder == null)
        {
            platrfomsHolder = new GameObject("PlatformHolder").transform;
            cointBoxHolder = new GameObject("CointBoxHolder").transform;
        }
        if (transform.position.x < pointGaneratePlatform.position.x)
        {
            int indxPlatform = Random.Range(0, thePlatform.Length);   

            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + platformWidth[indxPlatform] + distanceBetween, heightChange, transform.position.z);

            GameObject newPlatform = Instantiate(thePlatform[indxPlatform], transform.position, transform.rotation);
            newPlatform.transform.SetParent(platrfomsHolder);

            int subject = Random.Range(0, 4);
            if (subject == 0)
            {
                float positionX = newPlatform.transform.position.x + Random.Range(-platformWidth[indxPlatform] / 2, platformWidth[indxPlatform] / 2);
                Vector3 positionCoint = new Vector3(positionX, newPlatform.transform.position.y + 0.6f, newPlatform.transform.position.z);

                GameObject newCoint = Instantiate(theCoins, positionCoint, transform.rotation);
                newCoint.transform.SetParent(cointBoxHolder);
            }
            else if (subject == 2)
            {
                float positionX = newPlatform.transform.position.x + Random.Range(-platformWidth[indxPlatform] / 3.5f, platformWidth[indxPlatform] / 2.5f);
                Vector3 positionCoint = new Vector3(positionX, newPlatform.transform.position.y + 0.5f, newPlatform.transform.position.z);

                GameObject newBox = Instantiate(Boxes[Random.Range(0,Boxes.Length)], positionCoint, transform.rotation);
                newBox.transform.SetParent(cointBoxHolder);
            }
        }
	}

    public void DestroyAllStuff()
    {
        Destroy(cointBoxHolder.gameObject);
        Destroy(platrfomsHolder.gameObject);
    }
}
