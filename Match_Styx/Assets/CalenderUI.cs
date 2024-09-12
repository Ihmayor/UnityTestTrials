using TMPro;
using UnityEngine;

public class CalenderUI : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public GameCycle _game;
    public void Update()
    {
        if (textElement == null)
            return;

        textElement.text = $"Days Left: ({_game.DaysPassed}/10)\n           /";
    }
}
