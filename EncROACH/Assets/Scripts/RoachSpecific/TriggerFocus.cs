using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TriggerFocus : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject selfCam;
     void OnTriggerEnter(Collider other)
    {
        playerCam.SetActive(false);
        selfCam.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        playerCam.SetActive(true);
        selfCam.SetActive(false);
    }
}
