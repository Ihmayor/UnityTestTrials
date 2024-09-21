using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarmZone : MonoBehaviour
{
    [SerializeField]
    GameCycleAsset _game;

    private void Start()
    {
        _game.OnWarmZoneExpanded.AddListener(ExpandWarmZone);
    }

    private void ExpandWarmZone()
    {
        GetComponent<BoxCollider>().size += new Vector3(40, 0, 0);
        _game.OnWarmZoneEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            _game.OnWarmZoneExit.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            _game.OnWarmZoneEnter.Invoke();
        }
    }
}
