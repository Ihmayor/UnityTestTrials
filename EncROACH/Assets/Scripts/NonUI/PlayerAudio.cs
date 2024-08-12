using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private Vector3 prevPosition;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        prevPosition = transform.position;
    }

    void LateUpdate()
    {
        if (prevPosition != transform.position && !m_AudioSource.isPlaying)
        {
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }

        if (!Input.anyKey && m_AudioSource.isPlaying)
        {
            m_AudioSource.Stop();
        }
        


        prevPosition = transform.position;
    }




}
