using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ChainController : MonoBehaviour
{
    public GameObject ConnectionPointA;
    public GameObject ConnectionPointB;

    public int DistanceLimit = 20;
    public float PullBackFactor = 0.4f;


    private Vector3 prevPositionA;
    private Vector3 prevPositionB;


    // Start is called before the first frame update
    void Start()
    {
        prevPositionA = ConnectionPointA.transform.position;
        prevPositionB = ConnectionPointB.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        //Get Player Info
        Vector3 positionA = ConnectionPointA.transform.position;
        Vector3 positionB = ConnectionPointB.transform.position;
        float diff = Vector3.Distance(positionA, positionB);

        //Follow Players
        Vector3 recenterPosition = ((positionA - positionB)/2) + positionB;
        transform.position = recenterPosition;

        //Rotate
        Vector3 direction = positionA - positionB;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.localRotation.x, -angle, transform.localRotation.z);

        //Adjust Size
        transform.localScale = new Vector3(diff, transform.localScale.y, transform.localScale.z);

        //Chain
        if (diff > DistanceLimit)
        {
            ConnectionPointA.transform.position = Vector3.MoveTowards(positionA, transform.position, PullBackFactor);
            ConnectionPointB.transform.position = Vector3.MoveTowards(positionB, transform.position, PullBackFactor);
        }

    }
}
