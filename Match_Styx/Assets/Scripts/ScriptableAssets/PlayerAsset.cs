using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Custom/PlayerAsset")]
public class PlayerAsset : ScriptableObject
{
    public bool IsFreezing;
    public bool IsMoving;
    public bool IsReading;
    public bool IsInteracting;
    public bool IsDead;
    public bool IsOutside;

    //0-1 Scale for Warmth
    public float WarmthMeter;


    public readonly int LIMIT_OF_MATCHES = 5;

    public int NumOfMatches = 1;
    public UnityEvent OnPlayerFreeze;
    public UnityEvent OnPlayerThaw;

    public UnityEvent OnMatchLit;
    public UnityEvent OnDeath;

    public void AddMatch(int increaseFactor = 1)
    {
        if (NumOfMatches + increaseFactor < LIMIT_OF_MATCHES)
        {
            NumOfMatches += increaseFactor;
        }
        else
        {
            NumOfMatches = LIMIT_OF_MATCHES;
        }
    }

    public void UseMatch()
    {
        if (NumOfMatches != 0)
        {
            NumOfMatches -= 1;
            OnMatchLit.Invoke();
            WarmthMeter = 1;
        }
    }

    public void ResetValues()
    {
        IsDead = false;
        IsMoving = false;
        IsReading = false;
        IsFreezing = false;
        IsOutside = true;
        IsInteracting = false;

        WarmthMeter = 1;
        NumOfMatches = 1;
    }
}
