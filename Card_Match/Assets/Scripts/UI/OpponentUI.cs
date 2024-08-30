using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentUI : MonoBehaviour
{
    [SerializeField]
    GameStateAsset _gameState;

    [SerializeField]
    OpponentExampleAsset _opponentExample;


    public GameObject FullHand, LeftHand, RightHand, MiddleHand;
    public Vector3 _originalPosition;


    GameObject _loadedLeftHand, _loadedRightHand, _loadedMiddleHand;


    public void Start()
    {
        LoadUIHand(LeftHand, RightHand, MiddleHand);
        _originalPosition = FullHand.transform.position;
    }

    public void Awake()
    {
        LeanTween.moveX(FullHand, transform.position.x - 50, 0.7f);
    }

    void LoadUIHand(GameObject pLeft, GameObject pRight, GameObject pMiddle)
    {
        _loadedLeftHand   = Instantiate(_opponentExample.leftPrefabs[0], pLeft.transform);
        _loadedRightHand  = Instantiate(_opponentExample.rightPrefabs[0], pRight.transform);
        _loadedMiddleHand = Instantiate(_opponentExample.middlePrefabs[0], pMiddle.transform);
    }

    public void ApplyGeneratedPrompts()
    {
        List<GameObject> opponentCards = new List<GameObject>() { _loadedLeftHand, _loadedMiddleHand, _loadedRightHand };

        int randomIndex = Random.Range(0, opponentCards.Count);
        GameObject cardOne = opponentCards[randomIndex];
        opponentCards.RemoveAt(randomIndex);

        randomIndex = Random.Range(0, opponentCards.Count);
        GameObject cardTwo = opponentCards[randomIndex];


        PromptManager.ApplyPrompts(cardOne, cardTwo, _gameState);
    }

    private void FixedUpdate()
    {
        if (_gameState != null &&
             _gameState.phase == GameStateAsset.Phase.Scramble &&
             gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
