using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    [SerializeField]
    GameStateAsset gameState;

    [SerializeField]
    Text text;
    [SerializeField]
    GameObject BG;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    GameObject startMenu;

    bool _isOpen;

    List<string> _list = new List<string>()
    {
        "Your goal in this game is to confuse your opponent while deducing what changes your opponent has made.",
        "You are in the decorate phase!\r\n\n\rGo ahead! Decorate your cards! Click the envelope when you're done.",
        "You are in the memorize phase. Memorize how your opponent has decorated their cards.\r\n\r\nDon't let them sneak some changes by you!",
        "You are in the scramble phase. Pick exactly two cards to scramble.\r\n\r\nPrompt cards will appear at the bottom telling you want you must keep, lose, add, move",
        "You are in the callout phase. Flip over the cards you think were your opponent's prompts. Pick exactly ONE of EACH. Don't flip any if you want to skip the guess.\r\n\r\n",
        "Let's see who has deduced the prompts correctly!\r\nCorrect-Lose = 2 Points\nCorrect-Skip = 1 Points\nAny Ties = 0 Points\r\n\r\nMost points win! "
    };
    public void Awake()
    {
        BG.SetActive(false);
    }
    void Update()
    {
        if (startMenu != null &&
            startMenu.activeSelf)
        {
            text.text = _list[0];
        }
        else
        {
            switch (gameState.phase)
            {
                case GameStateAsset.Phase.Decorate:
                    text.text = _list[1];
                    break;
                case GameStateAsset.Phase.Memorize:
                    text.text = _list[2];
                    break;
                case GameStateAsset.Phase.Scramble:
                    text.text = _list[3];
                    break;
                case GameStateAsset.Phase.Callout:
                    text.text = _list[4];
                    break;
                case GameStateAsset.Phase.Compare:
                    text.text = _list[5];
                    break;
                default:
                    return;
            }
        }
    }

    public void ToggleHelpPanel()
    {
        if (_isOpen)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
        _isOpen = !_isOpen;
    }

    void ClosePanel()
    {
        BG.SetActive(false);
        LeanTween.scale(panel, Vector2.zero, 0.2f).setEaseOutCubic();
    }

    void OpenPanel()
    {
        BG.SetActive(true);
        LeanTween.scale(panel, Vector2.one, 0.6f).setEaseOutBack();
    }

}
