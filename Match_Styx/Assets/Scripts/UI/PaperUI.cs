using UnityEngine;
using UnityEngine.UI;

public class PaperUI : MonoBehaviour
{
    [SerializeField]
    Text TextObject;

    [SerializeField]
    GameObject Panel;

    [SerializeField]
    PlayerAsset PlayerStats;

    [SerializeField]
    GameCycleAsset Game;

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
