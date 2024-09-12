using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coal : MonoBehaviour
{
    public Image bar;
    public ParticleSystem ashesParticle;
    public ParticleSystem smokeParticle;

    // Update is called once per frame
    void Update()
    {
        if (bar != null && ashesParticle != null)
        {
            if (bar.fillAmount == 1 && ashesParticle.isStopped)
            {
                smokeParticle.Play();
                ashesParticle.Play();
            }
            else if (bar.fillAmount < 0.3f)
            {
                smokeParticle.Stop();
                ashesParticle.Stop();
            }
        }
    }
}
