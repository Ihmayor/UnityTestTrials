using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Sticker")]
public class Sticker : ScriptableObject
{
    public string color;
    public float posX;
    public float posY;
    public float scale;
    public Sprite stickerSprite;
}
