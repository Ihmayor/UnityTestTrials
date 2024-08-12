using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponent<IClickHandler>().OnClick();
    }
}
