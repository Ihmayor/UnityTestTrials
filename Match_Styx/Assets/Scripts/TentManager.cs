using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentManager : MonoBehaviour
{
    public GameCycle _game;
    public PlayerStats _player;

    bool _isStaying;

    private void Awake()
    {
        _isStaying = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && 
            _isStaying)
        {
            _isStaying = false;
            _player.AddMatch();
            _player.IsOutside = false;
            _game.DaysPassed++;
            _game.OnDayPassed.Invoke();
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

}
