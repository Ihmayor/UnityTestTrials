using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField]
    Sprite _sprite;

    public string ConvertedUIPositionName { get; private set; }

    public bool isLocked { get; private set; }
    private PromptAsset.Affect AffectToReject;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void SetSprite(Sprite pSprite)
    {
        GetComponent<SpriteRenderer>().sprite = pSprite;
        _sprite = pSprite;
    }

    //Lock if this card is affected by Lose Prompt
    public void LockCard(PromptAsset.Affect affectedSticker )
    {
        isLocked = true;
        AffectToReject = affectedSticker;
    }

    private void OnTriggerEnter2D(Collider2D pCollision)
    {
        AddStickerOnCollision(pCollision);
    }

    private void AddStickerOnCollision(Collider2D pCollision)
    {
        //Add Stickers
        if (pCollision.gameObject.GetComponent<DraggableSticker>() != null)
        {
            if (isLocked)
            {
                StickerAsset sticker = pCollision.gameObject.GetComponent<DraggableSticker>().stickerAsset;
                if (sticker.color == AffectToReject || sticker.shape == AffectToReject)
                {
                    Destroy(pCollision.gameObject);
                    return;
                }
            }
            pCollision.gameObject.transform.parent = transform;
        }
    }


    public GameObject Convert(RectTransform pRect)
    {
        //Create a copy of the world space card as we convert the card to a canvas UI
        GameObject worldSpaceCard = Instantiate(gameObject, transform.parent);
        worldSpaceCard.name = pRect.gameObject.name + "(Clone)";
        worldSpaceCard.SetActive(false);
        worldSpaceCard.GetComponent<CardController>().ConvertedUIPositionName = pRect.gameObject.name;

        DraggableSticker[] stickerChildrenOriginal = gameObject.GetComponentsInChildren<DraggableSticker>();
        DraggableSticker[] stickerChildrenWorldSpace = worldSpaceCard.GetComponentsInChildren<DraggableSticker>();

        for (int i =0; i <worldSpaceCard.transform.childCount;i++)
        {
            DraggableSticker item = stickerChildrenWorldSpace[i];
            item.DisableFromSheetClone();
            item.stickerAsset = stickerChildrenOriginal[i].stickerAsset;
        }

        foreach (DraggableSticker sticker in GetComponentsInChildren<DraggableSticker>()) {
            sticker.Convert(pRect);
            sticker.transform.SetParent(transform, false);
        }

        CardUI defaultCard = pRect.gameObject.GetComponent<CardUI>();
        CardUI newUI = gameObject.AddComponent<CardUI>();
       
        newUI.SetCard(defaultCard.card);
        newUI.OnSelectCard.AddListener(() => {HandManager.SelectCard(gameObject); });
        newUI.OnDeselectCard.AddListener(() => { HandManager.DeselectCard(gameObject); });

        //card
        transform.position = Camera.main.WorldToScreenPoint(transform.position);
        Image UIImage = gameObject.GetComponent<Image>();
        if (UIImage == null)
        {
            UIImage = gameObject.AddComponent<Image>();
        }

        UIImage.sprite = _sprite;
        UIImage.rectTransform.sizeDelta = pRect.sizeDelta;

        //card Contents
        transform.SetParent(pRect.transform.parent, true);

        UIImage.rectTransform.position = pRect.position;
        UIImage.rectTransform.eulerAngles = pRect.eulerAngles;

        ConvertedUIPositionName = pRect.gameObject.name;

        return worldSpaceCard;
    }

}
