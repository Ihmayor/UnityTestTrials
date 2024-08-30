using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/GameState")]
public class GameStateAsset : ScriptableObject
{
    public float MemorizePhaseDuration = 10f;
    public Phase phase;
    public bool IsWin;
    
    public int NumOfDecoratedCards;

    public List<Sprite> CardBackingSprites = new List<Sprite>(3);

    public List<PromptAsset> AllPromptAssets = new List<PromptAsset>(24);

    public GameObject CardPrefab;
    public GameObject StickerPrefab;

    public PromptAsset Keep;
    public PromptAsset Lose;
    public PromptAsset Add;
    public PromptAsset Move;

    public PromptAsset OpponentKeep;
    public PromptAsset OpponentLose;
    public PromptAsset OpponentAdd;
    public PromptAsset OpponentMove;

    public enum Phase{
        Decorate,
        Memorize,
        Scramble,
        Callout,
        Compare
    }

    public void ResetDefaultValues()
    {
        phase = Phase.Decorate;
        NumOfDecoratedCards = 0;
        IsWin = false;
    }

    public void NextGamePhase()
    {
        switch (phase)
        {
            case Phase.Decorate:
                phase = Phase.Memorize; 
                break;
            case Phase.Memorize:
                phase = Phase.Scramble;
                break;
            case Phase.Scramble:
                phase = Phase.Callout;
                break;
            case Phase.Callout:
                phase = Phase.Compare;
                break;
            case Phase.Compare:
                phase = Phase.Decorate;
                break;
            default:
                phase = Phase.Decorate;
                break;
        }

    }

}
