using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorateUI : MonoBehaviour
{
    [SerializeField]
    GameState _gameState;

    private void FixedUpdate()
    {
        if (_gameState != null &&
            _gameState.phase == GameState.GamePhase.Memorize &&
            gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

    }
}
