using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorizeUI : MonoBehaviour
{
    [SerializeField]
    GameStateAsset _gameState;

    private void FixedUpdate()
    {
        if (_gameState != null &&
             _gameState.phase == GameStateAsset.Phase.Scramble &&
             gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}