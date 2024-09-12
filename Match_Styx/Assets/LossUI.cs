using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossUI : MonoBehaviour
{
    public GameObject LossPanel;
    public GameCycle Game;

    private void Awake()
    {
        Game.OnGameLoss.AddListener(() => { LossPanel.SetActive(true); });
    }
}
