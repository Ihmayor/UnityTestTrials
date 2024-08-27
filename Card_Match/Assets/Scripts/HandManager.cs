using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    static List<GameObject> CardsSubmitted;
    
    
    public void Start()
    {
        CardsSubmitted = new List<GameObject>();
    }
    public static void AddCard(GameObject card)
    {
        CardsSubmitted.Add(card);
    }

}
