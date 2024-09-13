using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentManager : MonoBehaviour
{
    [SerializeField]
    GameCycleAsset _game;

    [SerializeField]
    PlayerAsset _player;

    bool _isStaying;

    private void Awake()
    {
        _isStaying = false;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.J) && 
            _isStaying)
        {
            Sleep();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_player.IsOutside) return;
        _isStaying = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isStaying = false;
    }

    void Sleep()
    {
        //Regain matches
        _player.AddMatch();
        //Time Passes
        _game.PassDay();
        //Player goes inside tent
        _player.IsOutside = false;
        //Avoid Spamming
        _isStaying = false;
    }
}
