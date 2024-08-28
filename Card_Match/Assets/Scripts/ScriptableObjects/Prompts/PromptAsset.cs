using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Prompt")]
public class PromptAsset : ScriptableObject
{
    public List<PromptAsset> IncompatiblePrompts = new List<PromptAsset>();

    public Sprite PromptSprite;


    public enum Type 
    { 
        Move,
        Add,
        Lose,
        Keep
    }

    public enum Affect
    {
        Diamond,
        Heart,
        Green,
        Rose,
        Vanilla,
        Buff
    }
}
