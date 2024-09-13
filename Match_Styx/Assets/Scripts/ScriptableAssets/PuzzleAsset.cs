using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/PuzzleAsset")]
public class PuzzleAsset : ScriptableObject
{
    public int    NumOfAward;
    public string Answer;
    public string Description;
}
