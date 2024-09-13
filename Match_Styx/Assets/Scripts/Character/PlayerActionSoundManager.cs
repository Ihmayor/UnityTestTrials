using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionSoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip MatchLitSound;
    [SerializeField]
    AudioClip SnowStepSound;
    
    AudioSource _source;

    public PlayerAsset player;
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
        _source.loop = false;
        if (MatchLitSound != null)
        {
            _source.PlayOneShot(MatchLitSound);
        }
    }

    void PlaySnowStep()
    {
        _source.loop = true;
        _source.clip = SnowStepSound;
        _source.Play();
    }

}
