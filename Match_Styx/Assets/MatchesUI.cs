using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchesUI : MonoBehaviour
{
    public GameObject matches;
    public PlayerStats _playerStats;

    public List<GameObject> listOfMatchesUI;

    private void Update()
    {
        if (listOfMatchesUI.Count < _playerStats.NumOfMatches)
        {
            GameObject newMatch = Instantiate(matches);
            newMatch.transform.SetParent(transform, false);
            newMatch.transform.position += new Vector3((_playerStats.NumOfMatches - 1) * 100, 0, 0);
            listOfMatchesUI.Add(newMatch);
        }
        else if (listOfMatchesUI.Count > _playerStats.NumOfMatches)
        {
            GameObject foundMatch = listOfMatchesUI[listOfMatchesUI.Count - 1];
            listOfMatchesUI.Remove(foundMatch);
            Destroy(foundMatch);
        }
    }
}
