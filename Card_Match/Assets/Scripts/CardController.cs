using System;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    Sprite _sprite;
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.parent = transform;
    }
    public void Convert(RectTransform rect)
    {
        //Create a copy of the world space card as we convert the card to a canvas UI
        GameObject worldSpaceCard = Instantiate(gameObject, transform.parent);
        worldSpaceCard.SetActive(false);
        Destroy(worldSpaceCard.GetComponent<BoxCollider2D>());
        HandManager.AddCard(worldSpaceCard);
        foreach (var item in worldSpaceCard.GetComponentsInChildren<DraggableSticker>())
        {
            item.DisableFromSheetClone();
        }



        foreach (DraggableSticker sticker in GetComponentsInChildren<DraggableSticker>()) {
            sticker.Convert(rect);
            sticker.transform.SetParent(transform, false);
        }

        CardUI defaultCard = rect.gameObject.GetComponent<CardUI>();
        gameObject.AddComponent<CardUI>().card = defaultCard.card;

        //Card
        transform.position = Camera.main.WorldToScreenPoint(transform.position);
        Image UIImage = gameObject.AddComponent<Image>();
        UIImage.sprite = _sprite;

        UIImage.rectTransform.sizeDelta = rect.sizeDelta;

        //Card Contents
        transform.SetParent(rect.transform.parent, false);

        UIImage.rectTransform.position = rect.position;
        UIImage.rectTransform.eulerAngles = rect.eulerAngles;

    }

}
