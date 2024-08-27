using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentUI : MonoBehaviour
{
    [SerializeField]
    GameState _gameState;

    private void FixedUpdate()
    {
        if (_gameState != null &&
             _gameState.phase == GameState.GamePhase.Scramble &&
             gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
