using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingStage : MonoBehaviour
{
    public GameCycle Game;
    public PlayerStats Player;

    public GameObject OutsideCamera;

    void Update()
    {
        OutsideCamera.SetActive(Player.IsOutside);
    }
}
