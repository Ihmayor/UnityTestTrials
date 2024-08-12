using UnityEngine;

public class Pobby : MonoBehaviour
{
    public GameObject blink;
    public GameObject squash;
    public GameObject pot;

    public Animator potAnimator;
    public Animator squashAnimator;

    private void Update()
    {
        if (potAnimator != null && potAnimator.isActiveAndEnabled && potAnimator.GetBool("Grown"))
        {
            EnableSquash();
        }
        else if (squashAnimator != null && squashAnimator.isActiveAndEnabled  && squashAnimator.GetBool("Grown"))
        {
            EnableBlink();
        }

    }


    public void EnableSquash()
    {
        if (squash != null)
        {
            pot.SetActive(false);
            squash.SetActive(true);
        }
    }

    public void EnableBlink()
    {
        if (blink != null)
        {
            squash.SetActive(false);
            blink.SetActive(true);
        }

    }
}
