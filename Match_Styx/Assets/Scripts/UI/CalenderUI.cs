using TMPro;
using UnityEngine;

public class CalenderUI : MonoBehaviour
{
    [SerializeField] 
    TextMeshProUGUI textElement;
    
    [SerializeField] 
    GameCycleAsset _game;
    
    public void Update()
    {
        if (textElement == null)
            return;

        textElement.text = $"Days Left: ({_game.DaysPassed}/10)\n           /";
    }
}
