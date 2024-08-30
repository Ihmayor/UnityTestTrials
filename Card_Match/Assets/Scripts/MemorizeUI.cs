using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemorizeUI : MonoBehaviour
{
    [SerializeField]
    GameStateAsset _gameState;
    public Image progressBar;

    float timePassed;

    private void FixedUpdate()
    {
        if (_gameState != null &&
             _gameState.phase == GameStateAsset.Phase.Scramble &&
             gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        timePassed += Time.deltaTime;
        progressBar.fillAmount = 1 - (timePassed / _gameState.MemorizePhaseDuration);
    }

    private void Awake()
    {
        timePassed = 0;
    }

}