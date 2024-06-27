using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiantController : MonoBehaviour
{
    private const float AwakeDuration = 4f;
    public GameState GameState;

    public GameObject Hole;
    public GameObject Eye;

    private Vector3 OriginalHolePosition;

    private Vector3 OriginalEyePosition;

    private bool IsEyeOut = false;

    private readonly float AwakeFactor = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        OriginalHolePosition = Hole.transform.position;
        OriginalEyePosition = Eye.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.NoiseLevel > GameState.NoiseAcceptabilityLevel)
        {
            GameState.IsEyeVisible = true;
            GameState.NoiseLevel = 0;
        }
        if (GameState.IsEyeVisible != IsEyeOut)
        {
            Swap();
        }
    }

    void SleepEye()
    {
        if (GameState.NoiseLevel <= AwakeFactor)
        {
            GameState.IsEyeVisible = false;
        }
        else
        {
            //Keep Eye Awake if noise is still being made
            Invoke("SleepEye", AwakeDuration);
            GameState.NoiseLevel = 0;
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
                Invoke("SleepEye", AwakeDuration);
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

    private void OnApplicationQuit()
    {
        GameState.NoiseLevel = 0;
    }
}
