using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PobbyInitializer : MonoBehaviour
{
    public GameObject potGrow;
    private bool hasGrown = false;
    private void OnTriggerEnter(Collider other)
    {
        if (potGrow != null && !hasGrown)
        {
            potGrow.SetActive(true);
            hasGrown = true;
        }
    }
}
