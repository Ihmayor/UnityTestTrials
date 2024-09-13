using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    PlayerAsset Player;

    [SerializeField]
    GameObject OutsideUI;

    [SerializeField]
    GameObject InsideUI;

    void Update()
    {
        if (Player.IsOutside)
        {
            OutsideUI.SetActive(true);
            InsideUI.SetActive(false);
        }
        else
        {
            OutsideUI.SetActive(false);
            InsideUI.SetActive(true);
        }
    }
}