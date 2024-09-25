using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    public static UnityEvent<GameObject> OnGooseSelection = new UnityEvent<GameObject>();
    public static UnityEvent OnThrowItemLeft = new();
    public static UnityEvent OnThrowItemRight = new();
}
