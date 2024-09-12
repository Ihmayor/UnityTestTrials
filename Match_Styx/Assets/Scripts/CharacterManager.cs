using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public PlayerStats _playerStats;
    public float WarmthDecline = 0.02f;
    public float ThawFactor = 0.1f;

    Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        if (_playerStats.IsDead)
        {
            return;
        }

        if (!_playerStats.IsOutside)
            return;

        _animator.SetBool("IsFreezing", _playerStats.IsFreezing);

        if (_playerStats.IsFreezing)
        {
            _playerStats.WarmthMeter -= WarmthDecline;
            if (_playerStats.WarmthMeter < -1)
            {
                _playerStats.OnDeath.Invoke();
                Death();
            }
        }
        else
        {
            if (_playerStats.WarmthMeter < 1)
                _playerStats.WarmthMeter += ThawFactor;
        }

        if (Input.GetKeyDown(KeyCode.E) && _playerStats.IsOutside)
        {
            UseMatch();
        }
    }

    public void UseMatch()
    {
        if (!_playerStats.IsFreezing || _playerStats.NumOfMatches == 0)
        {
            return;
        }

        _playerStats.UseMatch();
    }
    public void Freeze()
    {
        _playerStats.IsFreezing = true;
    }

    public void Thaw()
    {
        _playerStats.IsFreezing = false;
    }

    private void OnApplicationQuit()
    {
        _playerStats.ResetValues();
    }

    public void Death()
    {
        _playerStats.WarmthMeter = 1;
    }
}
