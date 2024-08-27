using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleController : MonoBehaviour
{
    public IntVariable ScientistsLeft;
    public SphereCollider ScientistsCameraSphere;

    public GameObject WinUI;

    // Update is called once per frame
    void Update()
    {
        if (ScientistsLeft.Value <= 0 && ScientistsCameraSphere)
        {
            ScientistsCameraSphere.enabled = true;
            WinUI.SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        ScientistsLeft.Value = 3;
    }
}
