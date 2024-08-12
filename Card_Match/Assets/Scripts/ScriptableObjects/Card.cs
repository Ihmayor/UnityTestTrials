using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Card")]

public class Card : ScriptableObject
{
    public List<Sticker> stickers;
    public Sprite backgroundSprite;
}
