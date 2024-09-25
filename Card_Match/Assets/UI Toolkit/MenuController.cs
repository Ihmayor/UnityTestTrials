using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public VisualElement _ui;
    public Button leftButton;
    public Button rightButton;

    public GameObject GooseSilo;
    private void Awake()
    {
        _ui = GetComponent<UIDocument>().rootVisualElement;    
    }

    private void OnEnable()
    {
        leftButton = _ui.Q<Button>("LeftButton");
        leftButton.clicked += LeftButton_clicked;

        rightButton = _ui.Q<Button>("RightButton");
        rightButton.clicked += RightButton_clicked;
    }

    private void OnDisable()
    {
        leftButton.clicked -= LeftButton_clicked;
        rightButton.clicked -= RightButton_clicked;
    }

    private void LeftButton_clicked()
    {
        GameEvents.OnThrowItemLeft.Invoke();
    }

    private void RightButton_clicked()
    {
        GameEvents.OnThrowItemRight.Invoke();
    }
}
