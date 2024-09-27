using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    void Update()
    {
        if (transform.parent != null && transform.parent.tag.Contains("Player"))
        {
            CenterBox();
        }
    }
    
    private void CenterBox()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector3 positionAtCollision = transform.localPosition;
        transform.localPosition = new Vector3(-0.1f, positionAtCollision.y, positionAtCollision.z);
    }
}
