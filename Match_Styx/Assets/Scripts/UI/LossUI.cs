using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossUI : MonoBehaviour
{
    [SerializeField]
    GameObject LossPanel;

    [SerializeField]
    GameCycleAsset Game;

    private void Awake()
    {
        Game.OnGameLoss.AddListener(() => { LossPanel.SetActive(true); });
    }
}
