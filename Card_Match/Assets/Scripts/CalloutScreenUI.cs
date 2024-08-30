using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CalloutScreenUI : MonoBehaviour
{
    [SerializeField]
    CardDeckUI KeepDeck;
    [SerializeField]
    CardDeckUI LoseDeck;
    [SerializeField]
    CardDeckUI AddDeck;
    [SerializeField]
    CardDeckUI MoveDeck;

    [SerializeField]
    GuessAsset Guess;

    [SerializeField]
    UnityEvent OnCalloutSent;

    public void GetSelectedGuesses()
    {
        CalloutCardUI selectedKeep = KeepDeck.GetTopCard();
        CalloutCardUI selectedLose = LoseDeck.GetTopCard();
        CalloutCardUI selectedAdd  = AddDeck .GetTopCard();
        CalloutCardUI selectedMove = MoveDeck.GetTopCard();

        Guess.Keep = GetPromptAsset(selectedKeep.GetSprite());
        Guess.Lose = GetPromptAsset(selectedLose.GetSprite());
        Guess.Add  = GetPromptAsset(selectedAdd.GetSprite());
        Guess.Move = GetPromptAsset(selectedMove.GetSprite());
        OnCalloutSent.Invoke();
    }

    PromptAsset GetPromptAsset(Sprite pSprite)
    {
        return Resources.LoadAll<PromptAsset>("PromptCardAssets").Where(prompt => prompt.PromptSprite == pSprite).FirstOrDefault();
    }
}
