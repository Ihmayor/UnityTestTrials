using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/GameState")]
public class GameState : ScriptableObject
{

    public GamePhase phase;
    public bool IsWin;
    
    public int NumOfDecoratedCards;
    public List<Sprite> CardBackingSprites = new List<Sprite>(3);

    public GameObject CardPrefab;

    public enum GamePhase{
        Decorate,
        Memorize,
        Scramble,
        Callout,
        Compare
    }

    public void ResetDefaultValues()
    {
        phase = GamePhase.Decorate;
        NumOfDecoratedCards = 0;
        IsWin = false;
    }

    public void NextGamePhase()
    {
        switch (phase)
        {
            case GamePhase.Decorate:
                phase = GamePhase.Memorize; 
                break;
            case GamePhase.Memorize:
                phase = GamePhase.Scramble;
                break;
            case GamePhase.Scramble:
                phase = GamePhase.Callout;
                break;
            case GamePhase.Callout:
                phase = GamePhase.Compare;
                break;
            case GamePhase.Compare:
                phase = GamePhase.Decorate;
                break;
            default:
                phase = GamePhase.Decorate;
                break;
        }

    }

}
