using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] peelSounds;

    public void PlayPeelSound()
    {
        GetComponent<AudioSource>().PlayOneShot(peelSounds[Random.Range(0, peelSounds.Length)]);
    }



}
