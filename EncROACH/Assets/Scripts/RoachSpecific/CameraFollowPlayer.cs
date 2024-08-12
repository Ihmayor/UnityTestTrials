using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    public float maxDist = 4f;

    public float cameraYOffset = 10f;
    public float cameraZOffset = -20f;

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(
                                        transform.position, 
                                        new Vector3(target.position.x, target.position.y + cameraYOffset, target.position.z + cameraZOffset), 
                                        maxDist);
    }
}
