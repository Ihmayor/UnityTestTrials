using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearTextOnAwake : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = "";
    }
}
