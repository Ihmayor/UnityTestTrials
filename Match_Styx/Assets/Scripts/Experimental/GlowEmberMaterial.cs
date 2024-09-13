using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowEmberMaterial : MonoBehaviour
{
    public float minGlow;
    public float maxGlow;

    public Image progressBar;

    Material glowMaterial;
    float slope;
    private void Start()
    {
        slope = maxGlow - minGlow;
        glowMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float fillAmount = progressBar.fillAmount;
        if (fillAmount > 0.2)
        {
            glowMaterial.SetInt("_IsGlow", 0);
            glowMaterial.SetFloat("_ColorSwitch", minGlow + slope * (progressBar.fillAmount));
        }
        else
        {
            glowMaterial.SetInt("_IsGlow", 1);
        }

    }
}
