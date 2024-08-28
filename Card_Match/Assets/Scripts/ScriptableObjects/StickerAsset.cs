using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PromptAsset;

[CreateAssetMenu(menuName = "Custom/Sticker")]
public class StickerAsset : ScriptableObject
{
    public Affect color;
    public Affect shape;
    public float posX;
    public float posY;
    public Vector2 scale;
    public Sprite stickerSprite;

    public StickerAsset (GameObject gameObject)
    {
        stickerSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        posX = screenPoint.x;
        posY = screenPoint.y;
    }
}
