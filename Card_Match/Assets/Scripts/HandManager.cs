using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onMemorizePhaseStart;

    [SerializeField]
    private UnityEvent _onMemorizePhaseEnter;

    
    [SerializeField]
    private UnityEvent _onScramblePhaseEnter;

    [SerializeField]
    private UnityEvent _onScramblePhaseStay;

    [SerializeField]
    private UnityEvent _onCalloutPhase;

    private GameObject _currentCard;

    [SerializeField]
    GameStateAsset _gameState;

    private static readonly int TOTAL_CARDS_TO_DECORATE = 3;

    [SerializeField]
    List<RectTransform> CardUIPositions = new List<RectTransform>(TOTAL_CARDS_TO_DECORATE);


    public static Queue<GameObject> SelectedCardsUI = new Queue<GameObject>(2);

    private static List<GameObject> CardsSubmitted;

    private bool _isScrambling = false;
    private bool _isMemorizing = false;

    private static List<GameObject> CardsScrambled;

    public void Start()
    {
        CardsSubmitted = new List<GameObject>();
        _currentCard = Instantiate(_gameState.CardPrefab);
    }

    void Update()
    {
        if (_gameState.NumOfDecoratedCards == 3 && _gameState.phase == GameStateAsset.Phase.Decorate && !_isMemorizing)
        {
            _isMemorizing = true;
            StartCoroutine(MemorizePhase(1f, _gameState.MemorizePhaseDuration));
        }

        if (_gameState.phase == GameStateAsset.Phase.Scramble && !_isScrambling)
        {
            _isScrambling = true;
            _onScramblePhaseEnter.Invoke();
        }

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
        if (SelectedCardsUI.Count < 2)
        {
            //Invalid Card Submission
            return;
        }

        List<string> selectedPositions = SelectedCardsUI.Select(x => x.GetComponent<CardController>().ConvertedUIPositionName).ToList();
        List<GameObject> selectedCards = CardsSubmitted.Where(x => selectedPositions.Contains(x.GetComponent<CardController>().ConvertedUIPositionName)).ToList();

        _onScramblePhaseStay.Invoke();


        //First card;
        GameObject firstCard = selectedCards[0];
        GameObject secondCard = selectedCards[1];

        //Apply Prompt Limits
        PromptManager.ApplyPromptLimits(firstCard, _gameState.Keep, _gameState.Move, _gameState.Lose, _gameState.Add);
        PromptManager.ApplyPromptLimits(secondCard, _gameState.Keep, _gameState.Move, _gameState.Lose, _gameState.Add);

        //Add Self-Validator
        CardPromptValidator firstCardPromptValidator = firstCard.AddComponent<CardPromptValidator>();
        firstCardPromptValidator.SetupCardValidation(Instantiate(firstCard), _gameState);
        
        CardPromptValidator secondCardPromptValidator = secondCard.AddComponent<CardPromptValidator>();
        secondCardPromptValidator.SetupCardValidation(Instantiate(secondCard), _gameState);


        //Animate Cards onto screen
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
                    CardsScrambled = new List<GameObject>() { firstCard, secondCard };
                });
            });
    }

    public void CalloutPhase()
    {
        if (CardsScrambled.Where(x => x.GetComponent<SpriteRenderer>().color == Color.red).Count() > 0)
            return;
        foreach(GameObject card in CardsScrambled)
        {
            LeanTween.moveLocalY(card, card.transform.position.y + 10, 2f).setEaseOutQuart();
        }

        _gameState.NextGamePhase();
        _onCalloutPhase.Invoke();
    }

    IEnumerator MemorizePhase(float delayInSeconds, float phaseDurationInSeconds)
    {

        _gameState.phase = GameStateAsset.Phase.Memorize;
        _onMemorizePhaseStart.Invoke();

        PlayableDirector director = GetComponent<PlayableDirector>();
        director.Play();
        yield return new WaitForSeconds((float)director.duration - 1.2f);
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
