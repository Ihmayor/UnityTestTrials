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
    public float OffsetYFromModels = 1.57f;
    public float OffsetXFromModels = 0.5f;
    public float OffsetPadding = 0.1f;

    private Vector3 prevPositionA;
    private Vector3 prevPositionB;

    private float positionYALock;
    private float positionYBLock;

    // Start is called before the first frame update
    void Start()
    {
        prevPositionA = ConnectionPointA.transform.position;
        prevPositionB = ConnectionPointB.transform.position;

        positionYALock = prevPositionA.y;
        positionYBLock = prevPositionB.y;
    }

    // Update is called once per frame
    void Update()
    {


        //Get Player Info
        Vector3 positionA = ConnectionPointA.transform.position;
        Vector3 positionB = ConnectionPointB.transform.position;

        float diff = Vector3.Distance(positionA , positionB );

        //Follow Players
        Vector3 recenterPosition = ((positionA - positionB)/2) + positionB;
        recenterPosition.y = OffsetYFromModels;
        recenterPosition.x += OffsetXFromModels;
        transform.position = recenterPosition;

        //Rotate
        Vector3 direction = positionA - positionB;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.localRotation.x, -angle, transform.localRotation.z);

        //Adjust Size
        transform.localScale = new Vector3(Mathf.Abs(diff - OffsetPadding), transform.localScale.y, transform.localScale.z);

        //Chain
        if (diff > DistanceLimit)
        {
            ConnectionPointA.transform.position = Vector3.MoveTowards(positionA, new Vector3(transform.position.x, positionA.y, transform.position.z), PullBackFactor);
            ConnectionPointB.transform.position = Vector3.MoveTowards(positionB, new Vector3(transform.position.x, positionB.y, transform.position.z), PullBackFactor);
        }

    }
}
