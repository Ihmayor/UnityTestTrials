using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameCycle _mainGame;
    public PlayerStats _player;
    public void Awake()
    {
        _player.OnDeath.AddListener(PassDay);
        _mainGame.OnWritingComplete.AddListener(PlayerLeaves);
    }

    private void PlayerLeaves()
    {
        _player.IsOutside = true;
    }

    public void Update()
    {
        if (_mainGame.DaysPassed >= _mainGame.DAYS_ALLOWABLE)
        {
            _player.IsReading = false;
            _mainGame.OnGameLoss.Invoke();
            _player.IsDead = true;
        }
    }

    public void PassDay()
    {
        _mainGame.DaysPassed ++;
        _mainGame.OnDayPassed.Invoke();
    }

    private void OnApplicationQuit()
    {
        _mainGame.ResetValues();
    }
}
