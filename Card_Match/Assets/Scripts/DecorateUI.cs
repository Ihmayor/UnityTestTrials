using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorateUI : MonoBehaviour
{
    [SerializeField]
    GameStateAsset _gameState;

    private void FixedUpdate()
    {
        if (_gameState != null &&
            _gameState.phase == GameStateAsset.Phase.Memorize &&
            gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

    }
}
