using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfViewRenderer : MonoBehaviour
{
    private LineRenderer handle1;
    private LineRenderer handle2;

    private void Awake()
    {
        handle1 = GetComponent<LineRenderer>();
    }
}
