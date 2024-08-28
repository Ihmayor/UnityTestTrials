using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PromptUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    float _originalY;
    [SerializeField]
    float _hoverY;
    [SerializeField]
    float _speedY;

    public void OnPointerClick(PointerEventData eventData)
    {
        LeanTween.moveLocalY(gameObject, _originalY, _speedY * 1.5f);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.moveLocalY(gameObject, _hoverY, _speedY).setEaseInSine();
        transform.SetSiblingIndex(3);
    }

}
