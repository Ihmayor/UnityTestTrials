using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingStage : MonoBehaviour
{
    
    [SerializeField] 
    PlayerAsset Player;

    [SerializeField]
    GameObject OutsideCamera;

    void Update()
    {
        OutsideCamera.SetActive(Player.IsOutside);
    }
}
