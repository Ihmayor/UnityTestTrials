using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleController : MonoBehaviour
{
    public IntVariable ScientistsLeft;
    public SphereCollider ScientistsCameraSphere;

    // Update is called once per frame
    void Update()
    {
        if (ScientistsLeft.Value <= 0 && ScientistsCameraSphere)
        {
            ScientistsCameraSphere.enabled = true;
        }
    }

    private void OnApplicationQuit()
    {
        ScientistsLeft.Value = 3;
    }
}
