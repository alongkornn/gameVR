using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    public GameObject startPoint, playerPoint;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            playerPoint.transform.position = startPoint.transform.position;
        }
    }

    // Update is called once per frame

}
