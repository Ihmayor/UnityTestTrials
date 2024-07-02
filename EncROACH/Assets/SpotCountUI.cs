using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpotCountUI : MonoBehaviour
{
    public GameState gameState;
    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"Spot Count: {gameState.SpotCount}";
    }
}
