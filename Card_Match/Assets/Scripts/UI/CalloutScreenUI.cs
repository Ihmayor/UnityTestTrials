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

        Guess.Keep = selectedKeep.IsFlipped ? GetPromptAsset(selectedKeep.GetSprite()) : null;
        Guess.Lose = selectedLose.IsFlipped ? GetPromptAsset(selectedLose.GetSprite()) : null;
        Guess.Add  = selectedAdd.IsFlipped  ? GetPromptAsset(selectedAdd.GetSprite())   : null;
        Guess.Move = selectedMove.IsFlipped ? GetPromptAsset(selectedMove.GetSprite())  : null;

        Guess.OpponentKeep = GetRandomPrompt(PromptAsset.Type.Keep);
        Guess.OpponentLose = GetRandomPrompt(PromptAsset.Type.Lose);
        Guess.OpponentAdd  = GetRandomPrompt(PromptAsset.Type.Add);
        Guess.OpponentMove = GetRandomPrompt(PromptAsset.Type.Move);

        OnCalloutSent.Invoke();
    }

    PromptAsset GetPromptAsset(Sprite pSprite)
    {
        return Resources.LoadAll<PromptAsset>("PromptCardAssets").Where(prompt => prompt.PromptSprite == pSprite).FirstOrDefault();
    }

    PromptAsset GetRandomPrompt(PromptAsset.Type type)
    {
        int randomIndex = Random.Range(0, 6);
        return Resources.LoadAll<PromptAsset>("PromptCardAssets").Where(prompt => prompt.PromptType == type).ToList()[randomIndex];
    }


}
