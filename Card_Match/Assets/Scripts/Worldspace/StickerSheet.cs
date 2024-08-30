using UnityEngine;

public class StickerSheet : MonoBehaviour
{
    [SerializeField]
    float DistanceExitUp = 10f;
    public void ExitStage()
    {
        LeanTween.moveY(gameObject, transform.position.y + DistanceExitUp, 0.2f).setEaseInSine();
    }

    public void EnterStage()
    {
        LeanTween.moveY(gameObject, transform.position.y - DistanceExitUp, 0.2f).setEaseInSine();
    }
}
