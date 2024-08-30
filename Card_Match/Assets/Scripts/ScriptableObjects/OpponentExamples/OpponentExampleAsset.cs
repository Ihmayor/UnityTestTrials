using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/OpponentExampleAsset")]
public class OpponentExampleAsset : ScriptableObject
{
    public List<GameObject> leftPrefabs;
    public List<GameObject> rightPrefabs;
    public List<GameObject> middlePrefabs;
}
