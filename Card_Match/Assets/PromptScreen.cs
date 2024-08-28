using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptScreen : MonoBehaviour
{
    public PromptUI       KeepPosition;
    public PromptUI       LosePosition;
    public PromptUI       AddPosition;
    public PromptUI       MovePosition;

    public GameStateAsset MainGame;

    // Update is called once per frame
    void Update()
    {
        if (MainGame     != null &&
            KeepPosition != null &&
            LosePosition != null &&
            AddPosition  != null &&
            MovePosition != null)
        {
            KeepPosition.SetPromptCard(MainGame.Keep);
            LosePosition.SetPromptCard(MainGame.Lose);
            AddPosition .SetPromptCard(MainGame.Add);
            MovePosition.SetPromptCard(MainGame.Move);
        }
    }
}
