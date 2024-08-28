using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardComparer : MonoBehaviour
{
    [SerializeField]
    GameObject original;
    [SerializeField]
    GameObject match;
    [SerializeField]
    GameObject changed;

    public void Compare()
    {
        string serializedOriginal = SerializeCardString(original);
        string serializedMatch = SerializeCardString(match);
        string serializedChanged = SerializeCardString(changed);

        if (serializedOriginal == serializedMatch)
        {
            match.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            match.GetComponent<SpriteRenderer>().color = Color.red;
        }


        if (serializedOriginal ==  serializedChanged)
        {
            changed.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            changed.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public static List<Tuple<string, float, float>> SerializeCard(GameObject pCard)
    {
        List<Tuple<string, float,float>> serializeList = new List<Tuple<string, float, float>>();
        for (int i = 0; i < pCard.transform.childCount; i++)
        {
            GameObject stickerItem = pCard.transform.GetChild(i).gameObject;
            StickerAsset stickerAsset = stickerItem.GetComponent<DraggableSticker>().stickerAsset;
            serializeList.Add(new Tuple<string, float, float>
                (
                    stickerAsset.name,
                    stickerItem.transform.localPosition.x,
                    stickerItem.transform.localPosition.y
                ));

        }
        return serializeList;
    }

    public static string SerializeCardString(GameObject pCard)
    {
        string serialization = "";
        for (int i = 0; i < pCard.transform.childCount; i++)
        {
            GameObject stickerItem = pCard.transform.GetChild(i).gameObject;
            StickerAsset stickerAsset = stickerItem.GetComponent<DraggableSticker>().stickerAsset;
            serialization += $"[{stickerAsset.name},{stickerItem.transform.localPosition.x},{stickerItem.transform.localPosition.y}]";
        }
        return serialization;
    }
}
