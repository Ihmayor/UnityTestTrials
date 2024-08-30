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
        Instantiate(_opponentExample.leftPrefabs[0], pLeft.transform);
        Instantiate(_opponentExample.rightPrefabs[0], pRight.transform);
        Instantiate(_opponentExample.middlePrefabs[0], pMiddle.transform);
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
