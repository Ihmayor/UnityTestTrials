using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HallwayCamera : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    public IntVariable ScientistsFound;
    public void LateUpdate()
    {
        textMesh.text = "Unknown Visible: " + ScientistsFound.Value;
    }
   
}
