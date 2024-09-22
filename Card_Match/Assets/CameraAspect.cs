using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    public bool IsMaintainWidth = true;
    [Range(-1, 1)]
    public int adaptPosition;
    float defaultWidth, defaultHeight;

    Vector3 CameraPos;
    void Start()
    {
        CameraPos = Camera.main.transform.position;
        defaultHeight = 5.42f;
        defaultWidth = 5.42f;
    }
    void Update()
    {   
        if (IsMaintainWidth)
        {
            Camera.main.orthographicSize = defaultHeight / Camera.main.aspect;
            Camera.main.transform.position = new Vector3(CameraPos.x, adaptPosition * (defaultHeight - Camera.main.orthographicSize), CameraPos.z);
        }
        else
        {
            Camera.main.transform.position = new Vector3(adaptPosition * (defaultWidth - Camera.main.orthographicSize * Camera.main.aspect), CameraPos.y, CameraPos.z);
        }
    }
}
