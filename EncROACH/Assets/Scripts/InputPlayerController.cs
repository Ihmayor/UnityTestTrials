using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerController : PlayerController
{
    override internal Vector3 GetDirection()
    {
        float hoz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        return new Vector3(hoz * moveSpeed, 0, vert * moveSpeed);
    }

}
