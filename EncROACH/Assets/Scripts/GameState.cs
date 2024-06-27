using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/GameState")]
public class GameState : ScriptableObject
{
    public float NoiseLevel;
    public bool IsEyeVisible;

    [Range(0.5f, 4)]
    public float NoiseAcceptabilityLevel; 
}
