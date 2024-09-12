using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionSoundManager : MonoBehaviour
{
    public AudioClip matchLitSound;
    public AudioClip snowStepSound;
    AudioSource _source;

    public PlayerStats player;
    private void Awake()
    {
        player.OnMatchLit.AddListener(PlayMatchLitSound);
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (player.IsFreezing &&
            player.IsMoving &&
            !_source.isPlaying)
        {
            PlaySnowStep();
        }

        if (!player.IsFreezing ||
            !player.IsMoving)
            _source.Stop();
            
        
            
    }

    void PlayMatchLitSound()
    {
        _source.PlayOneShot(matchLitSound);
    }

    void PlaySnowStep()
    {
        _source.loop = true;
        _source.clip = snowStepSound;
        _source.Play();
    }

}
