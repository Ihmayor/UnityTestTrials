using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BasicPopupButtonUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float originalRotateZ = 0f;
    void Awake()
    {
        originalRotateZ = transform.localRotation.eulerAngles.z;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        LeanTween.pause(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.rotateZ(gameObject, 0, 0.1f);
        LeanTween.scale(gameObject, Vector2.one * 1.2f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, Vector2.one, 0.1f);
        LeanTween.rotateZ(gameObject, originalRotateZ, 0.1f);
    }
}