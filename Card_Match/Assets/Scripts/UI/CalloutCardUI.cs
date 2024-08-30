using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalloutCardUI : MonoBehaviour, IPointerClickHandler
{
    public bool IsFlipped { get; private set; }
    [SerializeField]
    Sprite _front;
    [SerializeField]
    Sprite _back;
    Image _imageOnCard;

    [SerializeField]
    float FlipSpeed = 0.5f;

    void Start()
    {
        _imageOnCard = GetComponent<Image>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (LeanTween.isTweening(gameObject))
            return;
        IsFlipped = !IsFlipped;
        LeanTween.rotateY(gameObject, IsFlipped ? 90 : -90, FlipSpeed/2).setOnComplete(() => 
        {
            _imageOnCard.sprite = IsFlipped ? _front : _back;
            LeanTween.rotateY(gameObject,0, FlipSpeed);
            gameObject.transform.SetSiblingIndex(IsFlipped ? 5 : 0);
        });
    }

    public void SetCardSprites(Sprite pFront,  Sprite pBack)
    {
        _back = pBack;
        _front = pFront;
    }

    public Sprite GetSprite()
    {
        return _front;
    }

}
