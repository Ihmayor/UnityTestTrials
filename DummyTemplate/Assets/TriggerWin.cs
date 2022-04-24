using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWin : MonoBehaviour
{
    public BoolVariable GameState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
            GameState.Value = true;
    }

    private void OnApplicationQuit()
    {
        GameState.Value = false;
    }

}
