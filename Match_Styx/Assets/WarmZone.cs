using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarmZone : MonoBehaviour
{
    public GameCycle _game;

    private void Start()
    {
        _game.OnWarmZoneExpanded.AddListener(ExpandWarmZone);
    }

    private void ExpandWarmZone()
    {
        GetComponent<BoxCollider>().size += new Vector3(40, 0, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            other.gameObject.GetComponent<CharacterMovement>().FreezeSpeed();
            other.gameObject.GetComponent<CharacterManager>().Freeze();
            _game.OnWarmZoneLeft.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            other.gameObject.gameObject.GetComponent<CharacterMovement>().ThawSpeed();
            other.gameObject.gameObject.GetComponent<CharacterManager>().Thaw();
            _game.OnWarmZoneEnter.Invoke();
        }
    }
}
