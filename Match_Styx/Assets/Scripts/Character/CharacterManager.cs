using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    PlayerAsset _playerStats;
    [SerializeField]
    float WarmthDecline = 0.02f;
    [SerializeField]
    float ThawFactor = 0.1f;

    Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();    
    }

    private void Awake()
    {
        _playerStats.OnPlayerFreeze.AddListener(Freeze);
        _playerStats.OnPlayerThaw.AddListener(Thaw);
    }

    private void Update()
    {
        if (_playerStats.IsDead)
        {
            return;
        }

        AnimatorStateInfo animInfo = _animator.GetCurrentAnimatorStateInfo(1);
        if (!_playerStats.IsOutside && animInfo.IsName("Default"))
        {
            _animator.SetTrigger("Fade");
            return;
        }
        else if (_playerStats.IsOutside && animInfo.IsName("FadeOut"))
        {
            _animator.SetTrigger("Fade");
        }
        else
        {
            _animator.ResetTrigger("Fade");
        }


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

        if (Input.GetKeyDown(KeyCode.E) && _playerStats.IsOutside && _playerStats.IsFreezing)
        {
            Debug.Log("Using Match!");
            UseMatch();
        }
    }

    public void UseMatch()
    {
        if (!_playerStats.IsFreezing || _playerStats.NumOfMatches == 0 || _playerStats.IsInteracting)
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
