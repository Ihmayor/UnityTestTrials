using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public PlayerStats Player;
    public GameObject OutsideUI;
    public GameObject InsideUI;
    // Update is called once per frame
    void Update()
    {
        if (Player.IsOutside)
        {
            OutsideUI.SetActive(true);
            InsideUI.SetActive(false);
        }
        else
        {
            OutsideUI.SetActive(false);
            InsideUI.SetActive(true);
        }
    }
}
