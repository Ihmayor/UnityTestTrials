using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Custom/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public bool IsFreezing;
    public bool IsMoving;
    public bool IsReading;
    public bool IsDead;
    public bool IsOutside;

    //0-1 Scale for Warmth
    public float WarmthMeter;


    public readonly int LIMIT_OF_MATCHES = 5;

    public int NumOfMatches { get; private set; }

    public UnityEvent OnMatchLit;
    public UnityEvent OnDeath;

    public void AddMatch()
    {
        if (NumOfMatches + 1 < LIMIT_OF_MATCHES)
        {
            NumOfMatches += 1;
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

        WarmthMeter = 1;
        NumOfMatches = 1;
    }
}
