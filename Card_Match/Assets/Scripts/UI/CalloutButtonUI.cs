using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CalloutButtonUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    UnityEvent _onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick.Invoke();
        LeanTween.pause(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.rotateZ(gameObject, 20, 0.1f).setLoopPingPong(-1);
        LeanTween.scale(gameObject, Vector2.one * 1.2f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.pause(gameObject);
        LeanTween.scale(gameObject, Vector2.one, 0.1f);
        LeanTween.rotateZ(gameObject, 0, 0.2f).setEaseInBounce();
    }

}
