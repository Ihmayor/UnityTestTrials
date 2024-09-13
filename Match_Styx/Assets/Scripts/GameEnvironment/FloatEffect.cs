using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    [SerializeField]
    public float FloatFactor;
    
    [SerializeField]
    public float FloatSpeed;

    private void Awake()
    {
        LeanTween.moveY(gameObject, FloatFactor, FloatSpeed).setLoopPingPong();
    }
}
