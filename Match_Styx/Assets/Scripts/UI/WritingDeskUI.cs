using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WritingDesk : MonoBehaviour
{
    public GameCycleAsset Game;
    public Image WritingPanel; 
    public Text ExpectedJournalText;
    public TextMeshProUGUI ActualInputText;


    // Start is called before the first frame update
    void Awake()
    {
        Game.OnDayPassed.AddListener(() => { LoadNextJournalText(); });
    }

    public void LoadNextJournalText()
    {
        ExpectedJournalText.text = Game.GetNextJournalText();
        ActualInputText.text = "";
    }

    // Update is called once per frame
    void LateUpdate()
    {
        string expectedText = ExpectedJournalText.text;
        string actualText = ActualInputText.text;
        if (!string.IsNullOrEmpty(expectedText) && expectedText == actualText)
        {
            ActualInputText.text = "";
            Game.OnWritingComplete.Invoke();
            ExpectedJournalText.text = "";
        }
        else if (expectedText.Contains(actualText))
        {
            WritingPanel.color = new Color(1, 1, 1, 0.5f);
        }
        else 
        { 
            WritingPanel.color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (actualText.Length == 0)
                return;
            ActualInputText.text = actualText.Remove(actualText.Length - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Return)){
            ActualInputText.text += "\n";
        }
        else if (Input.anyKeyDown && !string.IsNullOrEmpty(expectedText))
        {
            string playerInput = Input.inputString;
            ActualInputText.text += playerInput;
        }
    }
}
