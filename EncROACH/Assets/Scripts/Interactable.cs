using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [TextArea(5, 40)]
    public string text;
    
    public GameObject UI;
    public TextMeshProUGUI textMesh;

    private void OnTriggerEnter(Collider other)
    {
        UI.SetActive(true);
        if (textMesh != null)
            textMesh.text = text;
    }

    private void OnTriggerExit(Collider other)
    {
        UI.SetActive(false);
        if (textMesh != null)
            textMesh.text = "";
    }
}
