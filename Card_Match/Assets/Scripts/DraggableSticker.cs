using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{

    float startX;
    float startY;
    bool isBeingHeld;

    [SerializeField]
    private UnityEvent _onStickerClick;

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
            isBeingHeld = false;
        }


        if (isBeingHeld)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPos.x - startX, newPos.y - startY, 0);
        }

    }

    void ClickDrag()
    {
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startX = clickPos.x - transform.position.x;
        startY = clickPos.y - transform.position.y;
        isBeingHeld = true;
    }

}
