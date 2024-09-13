using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameCycleAsset _mainGame;

    [SerializeField]
    PlayerAsset _player;
    
    public void Awake()
    {
        _player.OnDeath.AddListener(LoseDayFromDeath);

        _mainGame.OnOpenPuzzle    .AddListener(StopPlayerInteraction);
        _mainGame.OnClosePuzzle   .AddListener(ResumePlayerInteraction);
        _mainGame.OnPuzzleComplete.AddListener(AwardPuzzleReward);
        
        _mainGame.OnWritingComplete.AddListener(PlayerLeavesTent);
        
        _mainGame.OnWarmZoneEnter.AddListener(ThawPlayer);
        _mainGame.OnWarmZoneLeft .AddListener(FreezePlayer);
    }

    private void StopPlayerInteraction()
    {
        _player.IsInteracting = true;
    }

    private void ResumePlayerInteraction()
    {
        _player.IsInteracting = false;
    }

    private void AwardPuzzleReward(int awardAmount)
    {
        _player.IsInteracting = false;
        _player.AddMatch(awardAmount);
    }

    private void ThawPlayer()
    {
        _player.OnPlayerThaw.Invoke();
    }

    private void FreezePlayer()
    {
        _player.OnPlayerFreeze.Invoke();
    }

    private void PlayerLeavesTent()
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

    public void LoseDayFromDeath()
    {
        _mainGame.PassDay();
    }

    private void OnApplicationQuit()
    {
        _mainGame.ResetValues();
    }
}
