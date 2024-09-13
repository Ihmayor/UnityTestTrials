using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableItem : MonoBehaviour
{
    [SerializeField]
    PlayerAsset Player;

    [SerializeField]
    GameCycleAsset Game;

    [SerializeField]
    ReadableType type;

    bool _isReadable;

    public enum ReadableType
    {
        Puzzle,
        Paper
    }

    private void LateUpdate()
    {
        if (Player.IsInteracting)
            return;

        if (Input.GetKeyDown(KeyCode.J) && _isReadable)
        {
            switch (type) 
            {
                case ReadableType.Paper:
                    Player.IsReading = !Player.IsReading;
                    break;
                case ReadableType.Puzzle:
                    Game.OnOpenPuzzle.Invoke();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            _isReadable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            _isReadable = false;
            Player.IsReading = false;
        }
    }
}
