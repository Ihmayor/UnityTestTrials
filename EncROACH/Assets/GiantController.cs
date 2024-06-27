using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiantController : MonoBehaviour
{
    public GameState GameState;

    public GameObject Hole;
    public GameObject Eye;

    private Vector3 OriginalHolePosition;

    private Vector3 OriginalEyePosition;

    private bool IsEyeOut = false;


    // Start is called before the first frame update
    void Start()
    {
        OriginalHolePosition = Hole.transform.position;
        OriginalEyePosition = Eye.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.IsEyeVisible != IsEyeOut)
        {
            Swap();
        }
    }

    private void Swap()
    {
        Vector3 currentEyePosition = Eye.transform.position; 
        Vector3 currentHolePosition = Hole.transform.position;

        if (GameState.IsEyeVisible)
        {
            if (OriginalHolePosition  == currentEyePosition && OriginalEyePosition == currentHolePosition)
            {
                IsEyeOut = true;
                return;
            }
            Eye.transform.position = Vector3.MoveTowards(currentEyePosition, OriginalHolePosition, Time.deltaTime);
            Hole.transform.position = Vector3.MoveTowards(currentHolePosition, OriginalEyePosition, Time.deltaTime);
        }
        else
        {
            if (OriginalHolePosition == currentHolePosition && OriginalEyePosition == currentEyePosition)
            {
                IsEyeOut = false;
                return;
            }
            Eye.transform.position = Vector3.MoveTowards(currentEyePosition, OriginalEyePosition, Time.deltaTime);
            Hole.transform.position = Vector3.MoveTowards(currentHolePosition, OriginalHolePosition, Time.deltaTime);
        }
    }
}
