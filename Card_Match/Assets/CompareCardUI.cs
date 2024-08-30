using System.Collections;
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


    public CompareCardUI PairedCard;
    Image _imageOnCard;
    Vector3 _originalPosition;
    
    bool _isFlipped;
    public bool IsWin { get; private set; }

    void Start()
    {
        _imageOnCard = GetComponent<Image>();
        _originalPosition = transform.position;        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (LeanTween.isTweening(gameObject) || _isFlipped)
            return;
        FlipCardToFront();
        PairedCard.FlipCardToFront();
        _isFlipped = true;
        
        float delayInSeconds = 4f;

        if (!IsWin)
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
        LeanTween.moveY(gameObject, _originalPosition.y, 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.moveY(gameObject, _originalPosition.y + _hoverDistance, 0.1f);
    }

    public IEnumerator MovePairTowardsSelf(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        if (IsWin && PairedCard.IsWin)
        {
            MovePairTowardsPile();
        }
        else
        {
            LeanTween.move(gameObject, WinPosition, 0.3f);
            LeanTween.move(PairedCard.gameObject, WinPosition + new Vector2(0, _hoverDistance), 0.15f);
        }
    }


    public void MovePairTowardsPile()
    {
        LeanTween.move(gameObject, PilePosition, 0.3f);
        LeanTween.move(PairedCard.gameObject, PilePosition + new Vector2(0, _hoverDistance), 0.15f);
    }

}
