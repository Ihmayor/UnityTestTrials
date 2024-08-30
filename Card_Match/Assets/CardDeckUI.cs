using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDeckUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite DeckBack;
    public List<PromptAsset> CardPromptSprites;

    bool _isOpen;

    Vector3 _firstCardPosition;

    public void OnPointerClick(PointerEventData eventData)
    {
        _isOpen = true;
        CalloutCardUI[] calloutCards = GetComponentsInChildren<CalloutCardUI>();
        for(int i = 0; i < calloutCards.Length;i++) 
        {
            CalloutCardUI card = calloutCards[i];
            card.enabled = true;
            LeanTween.moveY(card.gameObject, _firstCardPosition.y - (110 * i) - 40, 0.4f* (0.5f * i));
        }
    }

    public void CloseCards()
    {
        _isOpen = false;
        CalloutCardUI[] calloutCards = GetComponentsInChildren<CalloutCardUI>();
        for (int i = 0; i < calloutCards.Length; i++)
        {
            CalloutCardUI card = calloutCards[i];
            card.enabled = false;
            LeanTween.moveY(card.gameObject, _firstCardPosition.y, 0.4f * (0.5f * i));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isOpen)
            return;
        CalloutCardUI firstCard = GetComponentInChildren<CalloutCardUI>();
        LeanTween.moveY(firstCard.gameObject, _firstCardPosition.y - 20, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isOpen)
            return;
        CalloutCardUI firstCard = GetComponentInChildren<CalloutCardUI>();
        LeanTween.moveY(firstCard.gameObject, _firstCardPosition.y, 0.1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        CalloutCardUI[] calloutCards = GetComponentsInChildren<CalloutCardUI>();
        _firstCardPosition = calloutCards[0].gameObject.transform.position;

        for (int i = 0; i < calloutCards.Length; i++)
        {
            CalloutCardUI card = calloutCards[i];
            card.SetCardSprites(CardPromptSprites[i].PromptSprite, DeckBack);
            card.enabled = false;
            card.gameObject.transform.position = _firstCardPosition;
        }
    }
    public CalloutCardUI GetTopCard()
    {
        return GetComponentsInChildren<CalloutCardUI>().Last();
    }
}
