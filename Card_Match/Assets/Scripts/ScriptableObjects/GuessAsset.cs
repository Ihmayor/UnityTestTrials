using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/GuessAsset")]
public class GuessAsset : ScriptableObject
{
    public PromptAsset Keep;
    public PromptAsset Lose;
    public PromptAsset Add;
    public PromptAsset Move;

    public PromptAsset OpponentKeep;
    public PromptAsset OpponentLose;
    public PromptAsset OpponentAdd;
    public PromptAsset OpponentMove;
}
