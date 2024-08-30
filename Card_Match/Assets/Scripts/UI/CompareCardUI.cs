using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompareCardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Sprite _front;

    [SerializeField]
    float _hoverDistance;

    [SerializeField]
    float FlipSpeed = 0.5f;

    [SerializeField]
    Vector2 WinPosition;

    [SerializeField]
    Vector2 PilePosition;

    [SerializeField]
    GameObject winOpponent;

    [SerializeField]
    GameObject winPlayer;

    public CompareCardUI PairedCard;
    Image _imageOnCard;
    Vector3 _originalPosition;
    
    public bool IsFlipped { get; private set; }
    public bool IsEvaluated { get; private set; }
    public bool IsWin { get; private set; }

    void Start()
    {
        _imageOnCard = GetComponent<Image>();
        _originalPosition = transform.position;        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (LeanTween.isTweening(gameObject) || IsFlipped)
            return;
        IsFlipped = true;

        FlipCardToFront();
        PairedCard.FlipCardToFront();
        
        float delayInSeconds = 1.4f;

        if (IsWin)
        {
            StartCoroutine(MovePairTowardsSelf(delayInSeconds));
        }
        else
        {
            StartCoroutine(PairedCard.MovePairTowardsSelf(delayInSeconds));
        }
    }

    public void FlipCardToFront()
    {
        if (LeanTween.isTweening(gameObject))
            return;
        LeanTween.rotateY(gameObject, 90, FlipSpeed / 2).setOnComplete(() =>
        {
            _imageOnCard.sprite = _front;
            LeanTween.rotateY(gameObject, 0, FlipSpeed);
        });
    }

    public void SetupCard(Sprite pFront, bool pIsWin)
    {
        _front = pFront;
        IsWin = pIsWin;
    }

    public void PairCard(CompareCardUI setPair)
    {
        PairedCard = setPair;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (LeanTween.isTweening(gameObject) || IsFlipped || IsEvaluated)
            return;
        LeanTween.moveY(gameObject, _originalPosition.y, 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LeanTween.isTweening(gameObject) || IsFlipped || IsEvaluated)
            return;
        LeanTween.moveY(gameObject, _originalPosition.y + _hoverDistance, 0.1f);
    }

    public IEnumerator MovePairTowardsSelf(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        if (IsWin == PairedCard.IsWin)
        {
            MovePairTowardsPile();
        }
        else
        {
            LeanTween.moveLocal(gameObject, this.WinPosition, 0.3f).setOnComplete(() => { gameObject.transform.SetParent(winPlayer.transform); });
            LeanTween.moveLocal(PairedCard.gameObject, this.WinPosition + new Vector2(0, _hoverDistance), 0.15f).setOnComplete(() => { PairedCard.gameObject.transform.SetParent(winPlayer.transform); }); ;
        }
        yield return new WaitForSeconds(5f);
        IsEvaluated = true;
        yield return null;
    }

    public void MovePairTowardsPile()
    {
        LeanTween.moveLocal(gameObject, PilePosition, 0.3f);
        LeanTween.moveLocal(PairedCard.gameObject, PilePosition + new Vector2(0, _hoverDistance), 0.15f);
        LeanTween.rotateZ(gameObject, 85, 0.4f);
        LeanTween.rotateZ(PairedCard.gameObject, 96, 0.4f);
        IsEvaluated = true;
    }

}
