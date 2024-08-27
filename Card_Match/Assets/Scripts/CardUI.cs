using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    public Card CardAsset;
    bool _isSelected;

    public UnityEvent OnSelectCard = new UnityEvent();
    public UnityEvent OnDeselectCard = new UnityEvent();

    public void SetCard(Card pCard)
    {
        CardAsset = pCard;
        gameObject.transform.position = pCard.OriginalPosition;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        _isSelected = !_isSelected;
        MoveSelectedCard();
        if (_isSelected) 
        {
            OnSelectCard.Invoke();
        }
        else
        {
            OnDeselectCard.Invoke();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        LeanTween.moveLocal(gameObject, CardAsset.HoverDestination, 0.2f).setEase(LeanTweenType.pingPong);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        LeanTween.moveLocal(gameObject, CardAsset.OriginalPosition, 0.2f);
    }

    public void Deselect()
    {
        _isSelected = false;
        MoveSelectedCard();
    }

    private void MoveSelectedCard()
    {
        LeanTween.moveLocal(gameObject, _isSelected ? CardAsset.SelectedPosition : CardAsset.OriginalPosition, 0.2f);
        LeanTween.rotateZ(gameObject, _isSelected ? 0 : CardAsset.OriginalRotateZ, 0.2f).setEaseInBack();
        transform.parent.SetSiblingIndex(_isSelected ? 2 : 0);
    }
}
