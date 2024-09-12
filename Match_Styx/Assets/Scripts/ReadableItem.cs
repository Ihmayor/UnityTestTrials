using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadbleItem : MonoBehaviour
{
    bool _isReadable;
    public PlayerStats Player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && _isReadable)
        {
            Player.IsReading = !Player.IsReading;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            _isReadable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            _isReadable = false;
            Player.IsReading = false;
        }
    }
}
