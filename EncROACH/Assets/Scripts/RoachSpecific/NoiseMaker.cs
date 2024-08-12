using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{

    public GameState gameState;
    [Range(0.01f, 1.1f)]
    public float NoiseFactor; 

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            gameState.NoiseLevel += NoiseFactor;
        }
    }
}
