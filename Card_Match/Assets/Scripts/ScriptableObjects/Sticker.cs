using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Sticker")]
public class Sticker : ScriptableObject
{
    public string color;
    public float posX;
    public float posY;
    public Vector2 scale;
    public Sprite stickerSprite;

    public Sticker (GameObject gameObject)
    {
        stickerSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        posX = screenPoint.x;
        posY = screenPoint.y;
    }
}
