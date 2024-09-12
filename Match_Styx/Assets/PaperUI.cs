using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperUI : MonoBehaviour
{
    public Text TextObject;
    public GameObject Panel;
    public PlayerStats PlayerStats;
    public GameCycle Game;

    public void Awake()
    {
        Game.OnDayPassed.AddListener(() => { SetText(Game.GetNextDialogue()); });
        SetText(Game.GetNextDialogue());
    }
    public void Update()
    {
        if (PlayerStats.IsReading)
        {
            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }

    public void SetText(string pText)
    {
        TextObject.text = pText;
    }
}
