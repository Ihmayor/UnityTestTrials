using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{

    [SerializeField]
    private UnityEvent _onMemorizePhaseEnter;

    [SerializeField]
    private UnityEvent _onScramblePhaseEnter;

    private GameObject _currentCard;

    [SerializeField]
    GameState _gameState;

    [SerializeField]
    float MemorizePhaseDuration = 10f;

    private static readonly int TOTAL_CARDS_TO_DECORATE = 3;

    [SerializeField]
    List<RectTransform> CardUIPositions = new List<RectTransform>(TOTAL_CARDS_TO_DECORATE);

    static Queue<GameObject> SelectedCardsUI = new Queue<GameObject>(2);
    private static List<GameObject> CardsSubmitted;


    private bool _isScrambling = false;
    private bool _isMemorizing = false;

    public void Start()
    {
        CardsSubmitted = new List<GameObject>();
        _currentCard = Instantiate(_gameState.CardPrefab);
    }

    public void AddCard()
    {
        if (_currentCard == null || CardUIPositions.Count != TOTAL_CARDS_TO_DECORATE) {
            return;
        }

        if (_gameState.NumOfDecoratedCards >= CardUIPositions.Count)
            return;

        RectTransform cardUIPosition = CardUIPositions[_gameState.NumOfDecoratedCards];
        _currentCard.GetComponent<CardController>().Convert(cardUIPosition);

        _gameState.NumOfDecoratedCards++;

        bool IsThereMoreCardsToDecorate = _gameState.NumOfDecoratedCards < TOTAL_CARDS_TO_DECORATE;
        if (IsThereMoreCardsToDecorate)
        {
            CardsSubmitted.Add(_currentCard);
            GameObject newCard = Instantiate(_gameState.CardPrefab);
            newCard.GetComponent<CardController>().SetSprite(_gameState.CardBackingSprites[_gameState.NumOfDecoratedCards]);
            _currentCard = newCard;
        }
        else
        {
            _currentCard = null;
        }
    }

    public static void SelectCard(GameObject pCard)
    {
        if (SelectedCardsUI.Count >= 2)
        {
            GameObject card = SelectedCardsUI.Dequeue();
            CardUI cardUI = card.GetComponent<CardUI>();
            cardUI.Deselect();
        }
        SelectedCardsUI.Enqueue(pCard);
    }

    public static void DeselectCard(GameObject pCard)
    {
        SelectedCardsUI.Dequeue();
    }

    public void SubmittedSelectedCards()
    {
        List<string> selectedPositions = SelectedCardsUI.Select(x => x.name).ToList();
        List<GameObject> selectedCards = CardsSubmitted.Where(x => selectedPositions.Contains(x.GetComponent<CardController>().ConvertedUIPositionName)).ToList();

        //First Card;
        GameObject firstCard = selectedCards[0];
        GameObject secondCard = selectedCards[1];

        LeanTween.moveLocalX(firstCard, firstCard.transform.position.x - 40, 0.2f).setEaseInOutBack();
        LeanTween.moveLocalX(secondCard, secondCard.transform.position.x + 40, 0.2f).setEaseInOutBack();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameState.NumOfDecoratedCards == 3 && _gameState.phase == GameState.GamePhase.Decorate && !_isMemorizing)
        {
            _isMemorizing = true;
            StartCoroutine(MemorizePhase(5f, MemorizePhaseDuration));
        }

        if (_gameState.phase == GameState.GamePhase.Scramble && !_isScrambling)
        {
            _isScrambling = true;
            _onScramblePhaseEnter.Invoke();
            ScramblePhase();
        }

    }

    void ScramblePhase()
    {
        
        //Select the Cards

    }


    IEnumerator MemorizePhase(float delayInSeconds, float phaseDurationInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        _gameState.phase = GameState.GamePhase.Memorize;
        _onMemorizePhaseEnter.Invoke();

        //Load Cards
        yield return new WaitForSeconds(phaseDurationInSeconds);

        //EndPhase
        _gameState.NextGamePhase();
        yield return null;
    }

    private void OnApplicationQuit()
    {
        _gameState.ResetDefaultValues();
    }
}
