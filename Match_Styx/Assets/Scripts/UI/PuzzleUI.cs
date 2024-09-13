using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{
    [SerializeField]
    GameObject PuzzleMain;

    [SerializeField]
    Text PuzzleText;

    [SerializeField]
    TextMeshProUGUI UserInput;

    [SerializeField]
    GameCycleAsset Game;

    string _secretAnswer;
    int _puzzleAward;

    bool _isLoaded = false;

    private void Awake()
    {
        Game.OnOpenPuzzle.AddListener(ShowPuzzle);
    }

    public void ShowPuzzle()
    {
        PuzzleMain.SetActive(true);
        UserInput.text = "";
        if (!_isLoaded)
            SetPuzzleAndSecret();
    }

    public void SetPuzzleAndSecret()
    {
        PuzzleAsset puzzle = Game.GetNextPuzzle();
        PuzzleText.text = puzzle.Description;
        UserInput.text = "";
        _secretAnswer = puzzle.Answer;
        _puzzleAward = puzzle.NumOfAward;
        _isLoaded = true;
    }

    public void Update()
    {
        if (!PuzzleMain.activeSelf)
        {
            UserInput.text = "";
            return;
        }

        string inputText = UserInput.text;
       

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (inputText.Length == 0)
                    return;
                UserInput.text = inputText.Remove(inputText.Length - 1);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (_secretAnswer.ToLower() == inputText.ToLower())
                {
                    _isLoaded = false;
                    PuzzleMain.SetActive(false);
                    Game.OnPuzzleComplete.Invoke(_puzzleAward);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Game.OnClosePuzzle.Invoke();
                UserInput.text = "";
                PuzzleMain.SetActive(false);
            }
            else
            {
                string playerInput = Input.inputString;
                UserInput.text += playerInput;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PuzzleMain == null || !other.gameObject.tag.Contains("Player"))
            return;
        PuzzleMain.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (PuzzleMain == null || !other.gameObject.tag.Contains("Player"))
            return;
        PuzzleMain.SetActive(false);
    }
}
