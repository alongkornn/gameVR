using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject loginPoint;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = loginPoint.transform.position;
    }

    // Update is called once per frame
    // void Update()
    // {

    // }
}
