using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingCharacter : MonoBehaviour
{
    Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsWriting", true);
    }
}
