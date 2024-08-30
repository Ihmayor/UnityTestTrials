using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPromptValidator : MonoBehaviour
{
    private GameStateAsset _mainGame;
    private GameObject _originalCard;
    private void LateUpdate()
    {
        if (_mainGame.GameEnd || _originalCard == null || _mainGame.Keep == null || _mainGame.Move == null || _mainGame.Lose == null || _mainGame.Add == null)
            return;
        if (PromptManager.IsAllPromptsExecuted(_originalCard, 
                                                gameObject, 
                                                _mainGame.Keep, 
                                                _mainGame.Move, 
                                                _mainGame.Lose, 
                                                _mainGame.Add))
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void SetupCardValidation(GameObject pOriginalCard, GameStateAsset pGameStateAsset)
    {
        _originalCard = pOriginalCard;
        _mainGame     = pGameStateAsset;
    }

}
