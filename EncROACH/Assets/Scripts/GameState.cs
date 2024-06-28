using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/GameState")]
public class GameState : ScriptableObject
{
    public int SpotLimit;
    public int SpotCount;
    public bool IsFullySpotted;

    public float NoiseLevel;
    public bool IsEyeVisible;

    [Range(0.5f, 4)]
    public float NoiseAcceptabilityLevel;


    public void Reset()
    {
        SpotCount = 0;
        NoiseLevel = 0;
        IsEyeVisible = false;
        IsFullySpotted = false;
    }
}
