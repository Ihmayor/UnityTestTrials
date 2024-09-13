using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarmthMeterUI : MonoBehaviour
{
    [SerializeField]
    Image HeartBar;

    [SerializeField]
    Image SecondBar;

    [SerializeField]
    PlayerAsset _playerStats;

    // Update is called once per frame
    void Update()
    {
        if (HeartBar != null && SecondBar != null)
        {
            HeartBar.fillAmount = _playerStats.WarmthMeter;
            if (HeartBar.fillAmount == 0)
            {
                SecondBar.fillAmount = 1 - Mathf.Abs(_playerStats.WarmthMeter);
            }
            else
            {
                SecondBar.fillAmount = 1;
            }
        }
    }
}
