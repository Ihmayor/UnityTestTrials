using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenUI : MonoBehaviour
{
    public GameObject Win;
    public GameObject Lose;
    public GameStateAsset GameState;
    // Update is called once per frame
    void Update()
    {
        if (!GameState.GameEnd)
        {
            Win.SetActive(false);
            Lose.SetActive(false);
            return;
        }

        if (GameState.IsWin)
        {
            Win.SetActive(true);
        }
        else
        {
            Lose.SetActive(true);
        }
    }
}
