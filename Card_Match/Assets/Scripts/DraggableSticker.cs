using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DraggableSticker : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onStickerClick;

    [SerializeField]
    public StickerAsset stickerAsset;

    private Vector3 _originalPosition;

    private float _startX;
    private float _startY;
    private bool _isBeingHeld;

    private bool _isFromSheet;

    public void Awake()
    {
        _originalPosition = transform.position;
        _isFromSheet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //get the touch position
            Ray hitRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //check that the mouse is overtop this sticker
            RaycastHit2D hit = Physics2D.GetRayIntersection(hitRay, Mathf.Infinity);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                ClickDrag();
                _onStickerClick.Invoke(); 
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isBeingHeld = false;
        }


        if (_isBeingHeld)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPos.x - _startX, newPos.y - _startY, 0);
        }

    }
    
    void ClickDrag()
    {
        if (_isFromSheet)
        {
            GameObject newSticker = Instantiate(gameObject, transform.parent);
            newSticker.transform.position = _originalPosition;
            newSticker.GetComponent<DraggableSticker>().stickerAsset = stickerAsset;
        }
        _isFromSheet = false;

        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _startX = clickPos.x - transform.position.x;
        _startY = clickPos.y - transform.position.y;
        _isBeingHeld = true;
    }

    public void DisableFromSheetClone()
    {
        _isFromSheet = false;
    }

    public void Convert(RectTransform pRectTransform)
    {
        Destroy(GetComponent<Rigidbody2D>());
        transform.SetParent(pRectTransform.parent, false);

        transform.position = Camera.main.WorldToScreenPoint(transform.localPosition);

        Image UIImage = gameObject.GetComponent<Image>();
        if (gameObject.GetComponent<Image>() == null)
        {
            UIImage = gameObject.AddComponent<Image>();
        }

        UIImage.sprite = stickerAsset.stickerSprite;
        UIImage.preserveAspect = true;
        UIImage.rectTransform.sizeDelta = stickerAsset.scale;
    }

}
