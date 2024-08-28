using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField]
    float DistanceEnterDown = 10;


    public void EnterStage()
    {
        LeanTween.moveY(gameObject, transform.position.y - DistanceEnterDown, 0.2f).setEaseInSine();
    }

    public void ExitStage()
    {
        LeanTween.moveY(gameObject, transform.position.y + DistanceEnterDown, 0.2f).setEaseInSine();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DraggableSticker>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
