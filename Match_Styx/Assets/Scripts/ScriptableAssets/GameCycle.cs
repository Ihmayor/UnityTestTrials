using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Custom/GameCycle")]
public class GameCycle : ScriptableObject
{
    public int DaysPassed;
    public readonly int DAYS_ALLOWABLE = 10;

    public UnityEvent OnWarmZoneLeft;
    public UnityEvent OnWarmZoneEnter;
    public UnityEvent OnWarmZoneExpanded;

    public UnityEvent OnGameLoss;
    public UnityEvent OnDayPassed;
    public UnityEvent OnWritingComplete;

    [TextArea(10, 60)]
    public List<string> PaperDialogue;

    [TextArea(10, 20)]
    public List<string> JournalPage;

    int _readIndex;
    int _journalIndex;

    public void ResetValues()
    {
        _readIndex = 0;
        _journalIndex = 0;
        DaysPassed = 0;
        OnWarmZoneLeft.RemoveAllListeners();
        OnWarmZoneEnter.RemoveAllListeners();
    }

    public string GetNextDialogue()
    {
        return GetNextStringFromList(PaperDialogue, _readIndex++);
    }

    public string GetNextJournalText()
    {
        return GetNextStringFromList(JournalPage, _journalIndex++);
    }

    public string GetCurrentJournal()
    {
        return JournalPage[_journalIndex];
    }

    public string GetNextStringFromList(List<string> pList, int pIndex)
    {
        string dialogue = pList.Last();
        if (pIndex >= pList.Count)
            return dialogue;
        dialogue = pList[pIndex];
        return dialogue;
    }
}
