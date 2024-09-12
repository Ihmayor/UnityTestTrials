using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AmbienceController : MonoBehaviour
{
    public  AudioClip cold;
    public  AudioClip warm;

    static AudioSource _source;

    public GameCycle _game;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        if (_game != null)
        {
            _game.OnWarmZoneEnter.AddListener(SetWarm);
            _game.OnWarmZoneLeft.AddListener(SetFreeze);
        }
    }

    public void SetFreeze()
    {
        _source.clip = cold;
        TriggerSound();
    }

    public void SetWarm()
    {
        _source.clip = warm;
        TriggerSound(); 
    }

    public  void TriggerSound()
    {
        if (_source.isPlaying)
        {
            Fade();
        }
        _source.Play();
    }

    IEnumerator Fade()
    {
        yield return null;
        float volume = LeanTween.easeOutQuad(_source.volume, 0, Time.deltaTime);
        _source.volume = volume;
        while(_source.volume > 0)
        {
            volume = LeanTween.easeOutQuad(_source.volume, 0, Time.deltaTime);
            _source.volume = volume;
        }
        volume = LeanTween.easeOutQuad(_source.volume, 1, Time.deltaTime);
        while (_source.volume < 1)
        {
            volume = LeanTween.easeOutQuad(_source.volume, 1, Time.deltaTime);
            _source.volume = volume;
        }
    }

}
