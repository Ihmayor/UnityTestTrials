using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseSelectionManager : SelectionManager
{
    private enum GooseAction
    {
        Left,
        Right,
        Honk
    }

    private void OnEnable()
    {
        GameEvents.OnThrowItemLeft.AddListener(SelectedGooseThrowItemLeft);
        GameEvents.OnThrowItemRight.AddListener(SelectedGooseThrowItemRight);
    }

    private void OnDisable()
    {
        GameEvents.OnThrowItemLeft.RemoveListener(SelectedGooseThrowItemLeft);
        GameEvents.OnThrowItemRight.RemoveListener(SelectedGooseThrowItemRight);
    }

    void SelectedGooseThrowItemLeft()
    {
        Debug.Log(_selectedObject.name);
        Debug.Log("Thrown Left");
        ActOnSelected(GooseAction.Left);
    }

    void SelectedGooseThrowItemRight()
    {
        ActOnSelected(GooseAction.Right);
    }

    void ActOnSelected (GooseAction gooseAction)
    {
        GooseMovement goose = _selectedObject.GetComponentInChildren<GooseMovement>();
        if (goose != null)
        {
            switch (gooseAction)
            {
                case GooseAction.Left:
                    goose.ThrowLeft();
                    break;
                case GooseAction.Right:
                    goose.ThrowRight();
                    break;
                case GooseAction.Honk:
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.LogError("No Goose Element Found on Selected!");
        }
    }
}
