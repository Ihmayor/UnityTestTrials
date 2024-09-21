using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PromptUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    float _originalY;
    [SerializeField]
    float _hoverY;
    [SerializeField]
    float _speedY;

    bool _isSelected;

    public void Start()
    {
        _originalY = transform.localPosition.y;
        _hoverY = _originalY + 200;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _isSelected = !_isSelected;
        LeanTween.moveLocalY(gameObject, _isSelected ? _hoverY : _originalY, _speedY * 1.5f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        LeanTween.moveLocalY(gameObject, _originalY + 30, _speedY).setEaseInSine();
        transform.SetSiblingIndex(3);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isSelected)
            return;
        LeanTween.moveLocalY(gameObject, _originalY, _speedY).setEaseInQuad();
    }

    public void SetPromptCard(PromptAsset prompt)
    {
        GetComponent<Image>().sprite = prompt.PromptSprite;
    }
}
