using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject WinPanel;
    public GameState GameState;
    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && !GameState.IsEyeVisible)
        {
            WinPanel.SetActive(true);
            await Task.Delay(TimeSpan.FromSeconds(5.2f));
            Application.Quit();
        }
    }

}
