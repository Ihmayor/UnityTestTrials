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

    [SerializeField]
    private UnityEvent _onScramblePhaseStay;

    private GameObject _currentCard;

    [SerializeField]
    GameStateAsset _gameState;

    [SerializeField]
    float MemorizePhaseDuration = 10f;

    private static readonly int TOTAL_CARDS_TO_DECORATE = 3;

    [SerializeField]
    List<RectTransform> CardUIPositions = new List<RectTransform>(TOTAL_CARDS_TO_DECORATE);


    public static Queue<GameObject> SelectedCardsUI = new Queue<GameObject>(2);

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
        GameObject worldSpaceCardClone =  _currentCard.GetComponent<CardController>().Convert(cardUIPosition);
        CardsSubmitted.Add(worldSpaceCardClone);

        _gameState.NumOfDecoratedCards++;

        bool IsThereMoreCardsToDecorate = _gameState.NumOfDecoratedCards < TOTAL_CARDS_TO_DECORATE;
        if (IsThereMoreCardsToDecorate)
        {
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
        List<string> selectedPositions = SelectedCardsUI.Select(x => x.GetComponent<CardController>().ConvertedUIPositionName).ToList();
        List<GameObject> selectedCards = CardsSubmitted.Where(x => selectedPositions.Contains(x.GetComponent<CardController>().ConvertedUIPositionName)).ToList();

        _onScramblePhaseStay.Invoke();


        //First card;
        GameObject firstCard = selectedCards[0];
        GameObject secondCard = selectedCards[1];

        firstCard.SetActive(true);
        LeanTween
            .moveLocalX(firstCard, firstCard.transform.position.x - 3, 2f)
            .setEaseOutQuart()
            .setOnComplete(() => 
            {
                secondCard.SetActive(true);
                LeanTween
                .moveLocalX(secondCard, secondCard.transform.position.x + 3, 2f)
                .setEaseOutQuart()
                .setOnComplete(() => 
                { 
                    ScramblePhase(new List<GameObject>() { firstCard, secondCard }); 
                });
            });
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameState.NumOfDecoratedCards == 3 && _gameState.phase == GameStateAsset.Phase.Decorate && !_isMemorizing)
        {
            _isMemorizing = true;
            StartCoroutine(MemorizePhase(5f, MemorizePhaseDuration));
        }

        if (_gameState.phase == GameStateAsset.Phase.Scramble && !_isScrambling)
        {
            _isScrambling = true;
            _onScramblePhaseEnter.Invoke();
        }

    }

    void ScramblePhase(List<GameObject> cardsToScramble)
    {

    }


    IEnumerator MemorizePhase(float delayInSeconds, float phaseDurationInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        _gameState.phase = GameStateAsset.Phase.Memorize;
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
