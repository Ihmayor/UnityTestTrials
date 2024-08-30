using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompareScreenUI : MonoBehaviour
{
    [SerializeField]
    GameStateAsset MainGame;

    [SerializeField]
    GuessAsset GuessState;
    
    [SerializeField]
    CompareCardUI playerKeep;

    [SerializeField]
    CompareCardUI playerLose;

    [SerializeField]
    CompareCardUI playerAdd;

    [SerializeField]
    CompareCardUI playerMove;

    [SerializeField]
    CompareCardUI opponentKeep;

    [SerializeField]
    CompareCardUI opponentLose;

    [SerializeField]
    CompareCardUI opponentAdd;

    [SerializeField]
    CompareCardUI opponentMove;

    [SerializeField]
    public GameObject winOpponent, winPlayer;

    public Sprite AddNoGuessSprite;
    public Sprite MoveNoGuessSprite;
    public Sprite LoseNoGuessSprite;
    public Sprite KeepNoGuessSprite;

    void Awake()
    {
        LoadCards();
    }

    private void LateUpdate()
    {
        if (MainGame.GameEnd || MainGame.phase != GameStateAsset.Phase.Compare)
        {
            return;
        }

        int cardsEvaluated = new CompareCardUI[4] { playerAdd, playerMove, playerKeep, playerLose }.Where(card => card.IsEvaluated || card.IsFlipped).Count();
        if (cardsEvaluated == 4)
        {
            MainGame.GameEnd = true;
            int playerTotal = winPlayer.transform.childCount;
            int opponentTotal = winOpponent.transform.childCount;
            if (playerTotal!= 0 && playerTotal >= opponentTotal)
                MainGame.IsWin = true;
            else
                MainGame.IsWin = false;
        }
    }

    void LoadCards()
    {
        Sprite[] NoGuessSprites = new Sprite[4] { AddNoGuessSprite, MoveNoGuessSprite, KeepNoGuessSprite, LoseNoGuessSprite };

        CompareCardUI[] playerCompares   = new CompareCardUI[4] { playerAdd,   playerMove,   playerKeep,   playerLose };
        CompareCardUI[] opponentCompares = new CompareCardUI[4] { opponentAdd, opponentMove, opponentKeep, opponentLose };

        PromptAsset[] actualPlayerPrompt   = new PromptAsset[4] { MainGame.Add,         MainGame.Move,         MainGame.Keep,         MainGame.Lose };
        PromptAsset[] actualOpponentPrompt = new PromptAsset[4] { MainGame.OpponentAdd, MainGame.OpponentMove, MainGame.OpponentKeep, MainGame.OpponentLose };

        PromptAsset[] guessPlayerPrompt   = new PromptAsset[4] { GuessState.Add,         GuessState.Move,         GuessState.Keep,         GuessState.Lose };
        PromptAsset[] guessOpponentPrompt = new PromptAsset[4] { GuessState.OpponentAdd, GuessState.OpponentMove, GuessState.OpponentKeep, GuessState.OpponentLose };

        for (int i = 0; i < 4; i++)
        {
            playerCompares[i].SetupCard(guessPlayerPrompt[i] != null && guessPlayerPrompt[i].PromptSprite != null ? guessPlayerPrompt[i].PromptSprite : NoGuessSprites[i], 
                                        EvaluateGuess(actualOpponentPrompt[i], guessPlayerPrompt[i]));

            opponentCompares[i].SetupCard(guessOpponentPrompt[i] != null && guessOpponentPrompt[i].PromptSprite != null ? guessOpponentPrompt[i].PromptSprite : NoGuessSprites[i],
                                          EvaluateGuess(actualPlayerPrompt[i], guessOpponentPrompt[i]));

            playerCompares[i].PairCard(opponentCompares[i]);
            opponentCompares[i].PairCard(playerCompares[i]);
        }
    }

    public bool EvaluateGuess(PromptAsset actual, PromptAsset guess)
    {
        if (guess == null) return false;
        return actual.Equals(guess);
    }

}
