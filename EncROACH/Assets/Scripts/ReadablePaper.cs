using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadablePaper : MonoBehaviour
{
    public GameObject paperUI;
    [TextArea(10, 1000)]
    public string text;

    public TextMeshProUGUI textMesh;
    bool isOpen = false;

    bool isTriggered = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.J) && isTriggered)
        {
            textMesh.text = text;
            paperUI.SetActive(!isOpen);
            isOpen = !isOpen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (paperUI != null)
        {
            paperUI.SetActive(false);
        }
        isTriggered = false;
    }
}
