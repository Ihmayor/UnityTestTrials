using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField]
    Sprite _sprite;

    public string ConvertedUIPositionName { get; private set; }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void SetSprite(Sprite pSprite)
    {
        GetComponent<SpriteRenderer>().sprite = pSprite;
        _sprite = pSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.parent = transform;
    }
    public void Convert(RectTransform pRect)
    {
        //Create a copy of the world space CardAsset as we convert the CardAsset to a canvas UI
        GameObject worldSpaceCard = Instantiate(gameObject, transform.parent);
        worldSpaceCard.SetActive(false);
        Destroy(worldSpaceCard.GetComponent<BoxCollider2D>());
        foreach (var item in worldSpaceCard.GetComponentsInChildren<DraggableSticker>())
        {
            item.DisableFromSheetClone();
        }

        foreach (DraggableSticker sticker in GetComponentsInChildren<DraggableSticker>()) {
            sticker.Convert(pRect);
            sticker.transform.SetParent(transform, false);
        }

        CardUI defaultCard = pRect.gameObject.GetComponent<CardUI>();
        CardUI newUI = gameObject.AddComponent<CardUI>();
       
        newUI.SetCard(defaultCard.CardAsset);
        newUI.OnSelectCard.AddListener(() => { HandManager.SelectCard(gameObject); });
        newUI.OnDeselectCard.AddListener(() => { HandManager.DeselectCard(gameObject); });

        //Card
        transform.position = Camera.main.WorldToScreenPoint(transform.position);
        Image UIImage = gameObject.GetComponent<Image>();
        if (UIImage == null)
        {
            UIImage = gameObject.AddComponent<Image>();
        }

        UIImage.sprite = _sprite;
        UIImage.rectTransform.sizeDelta = pRect.sizeDelta;

        //Card Contents
        transform.SetParent(pRect.transform.parent, true);

        UIImage.rectTransform.position = pRect.position;
        UIImage.rectTransform.eulerAngles = pRect.eulerAngles;

        ConvertedUIPositionName = pRect.gameObject.name;
    }

}
