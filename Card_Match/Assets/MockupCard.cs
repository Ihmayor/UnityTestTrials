using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MockupCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<DraggableSticker> draggables = gameObject.GetComponentsInChildren<DraggableSticker>().ToList();
        foreach(DraggableSticker sticker in draggables)
        {
            sticker.DisableFromSheetClone();
        }
    }

    
}
