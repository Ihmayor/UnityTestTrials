using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

public class PushableObject : MonoBehaviour
{
    [SerializeField]
    GameCycleAsset Game;

    [SerializeField]
    Sprite IsPushedSprite;
    
    [SerializeField]
    Sprite DefaultSprite;

    [SerializeField]
    float PushFactor;

    SpriteRenderer _renderer;

    bool _isInWarmZone;

    SphereCollider _triggerCollider;


    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _triggerCollider = GetComponent<SphereCollider>();
        if (DefaultSprite == null)
            DefaultSprite = _renderer.sprite;
    }

    private void Update()
    {
        if (!_isInWarmZone)
            _renderer.sprite = IsPushedSprite;
        else
            _renderer.sprite = DefaultSprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Warm"))
        {
            _isInWarmZone = true;
        }
        if (_isInWarmZone)
            return;

        if (other.gameObject.name.Contains("Player"))
        {
            Game.OnWarmZoneEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Warm"))
        {
            _isInWarmZone = false;
        }

        if (_isInWarmZone)
            return;

        if (other.gameObject.name.Contains("Player"))
        {
            Game.OnWarmZoneExit.Invoke();
        }
    }

}
