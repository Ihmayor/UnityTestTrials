using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Card card;
    bool _isSelected;

    public void OnPointerClick(PointerEventData eventData)
    {
        _isSelected = !_isSelected;
        LeanTween.moveLocal(gameObject, _isSelected ? card.SelectedPosition : card.OriginalPosition, 0.2f);
        LeanTween.rotateZ(gameObject, _isSelected ? 0 : card.OriginalRotateZ, 0.2f).setEaseInBack();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        LeanTween.moveLocal(gameObject, card.HoverDestination, 0.2f).setEase(LeanTweenType.pingPong);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        LeanTween.moveLocal(gameObject, card.OriginalPosition, 0.2f);
    }
}
