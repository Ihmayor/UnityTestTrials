using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchesUI : MonoBehaviour
{
    [SerializeField]
    GameObject matches;

    [SerializeField]
    PlayerAsset _playerStats;

    [SerializeField]
    List<GameObject> listOfMatchesUI;

    private void Update()
    {
        if (listOfMatchesUI.Count < _playerStats.NumOfMatches)
        {
            int numOfMatchesToAdd = _playerStats.NumOfMatches - listOfMatchesUI.Count;

            for (int i = 0; i < numOfMatchesToAdd; i++)
            {
                GameObject newMatch = Instantiate(matches);
                newMatch.transform.SetParent(transform, false);
                listOfMatchesUI.Add(newMatch);
                newMatch.transform.position += new Vector3((listOfMatchesUI.Count - 1) * 100, 0, 0);
            }
        }
        else if (listOfMatchesUI.Count > _playerStats.NumOfMatches)
        {
            GameObject foundMatch = listOfMatchesUI[listOfMatchesUI.Count - 1];
            listOfMatchesUI.Remove(foundMatch);
            Destroy(foundMatch);
        }
    }
}
