using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MockupCard : MonoBehaviour
{
    void Start()
    {
        //For Mockup cards used for testing, disable the stickers from duplicating
        List<DraggableSticker> draggables = gameObject.GetComponentsInChildren<DraggableSticker>().ToList();
        foreach(DraggableSticker sticker in draggables)
        {
            sticker.DisableFromSheetClone();
        }
    }

    
}
