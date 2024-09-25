using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null && transform.parent.tag.Contains("Player"))
        {
            CenterBox();
        }
    }
    
    private void CenterBox()
    {
        Vector3 positionAtCollision = transform.localPosition;
        transform.localPosition = new Vector3(-0.1f, positionAtCollision.y, positionAtCollision.z);
    }
}
