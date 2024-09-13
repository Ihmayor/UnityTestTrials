using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    float speed;
    bool isRespawning;

    [SerializeField]
    PlayerAsset _playerStats;

    void Awake()
    {
        speed = 20;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        _playerStats.OnMatchLit.AddListener(BoostSpeed);
        _playerStats.OnDeath.AddListener(RespawnAtCamp);
        _playerStats.OnPlayerFreeze.AddListener(FreezeSpeed);
        _playerStats.OnPlayerThaw.AddListener(ThawSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerStats.IsDead     ||
            !_playerStats.IsOutside ||
            _playerStats.IsInteracting)
        {
            return;
        }

        float hoz = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(hoz / speed, 0);
        if (isRespawning)
        {
            return;
        }

        if (Mathf.Abs(hoz) > 0)
        {
            transform.eulerAngles = new Vector3(0, movement.x < 0 ? 180 : 0, 0);
            animator.SetBool("IsMoving", true);
            _playerStats.IsMoving = true;
            if (isRespawning)
                return;
            controller.Move(movement);
        }
        else if (movement.x == 0 && animator.GetBool("IsMoving"))
        {
            animator.SetBool("IsMoving", false);
            _playerStats.IsMoving = false;
            return;
        }
    }

    public void FreezeSpeed()
    {
        speed *= 10;
    }

    public void ThawSpeed()
    {
        speed = 20;
    }

    public void BoostSpeed()
    {
        StartCoroutine(UseBoost());
    }

    IEnumerator UseBoost()
    {
        float originalSpeed = speed;
        speed = 3;
        yield return new WaitForSeconds(0.01f);
        while (speed < originalSpeed)
        {
            speed += 0.1f;
        }
        yield return null;
    }
    public void RespawnAtCamp()
    {
        StartCoroutine(Respawning());
    }

    IEnumerator Respawning()
    {
        isRespawning = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = new Vector3(0, 0.6f);
        yield return new WaitForSeconds(0.3f);
        isRespawning = false;
    }
}
