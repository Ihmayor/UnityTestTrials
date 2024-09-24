using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseMovement : MonoBehaviour
{

    [SerializeField]
    float speed = 0.1f;

    private bool _hasFlipped;
    private bool _isTurning;
    private bool _isAnimationWalking;
    private Animator _animator;

    private const string WALKING_ANIMATION_BOOL_NAME = "IsWalking";
    private const string HOLDING_ANIMATION_BOOL_NAME = "IsHolding";
    private const string HONKING_ANIMATION_TRIGGER_NAME = "Honk";
    private const string WALK_ANIMATION_STATE = "walk";

    private void Start()
    {
       _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_isTurning)
        {
            AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
            if (state.IsName(WALK_ANIMATION_STATE))
                transform.position += new Vector3(_hasFlipped ? speed : -speed, 0, 0);
            if (Input.GetKeyUp(KeyCode.J))
                ToggleMoving();
        }
    }

    void ToggleMoving()
    {
        bool currentWalkingState = _animator.GetBool(WALKING_ANIMATION_BOOL_NAME);
        _animator.SetBool(WALKING_ANIMATION_BOOL_NAME, !currentWalkingState);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tagOfCollider = collision.collider.tag;
        switch (tagOfCollider)
        {
            case "Wall":
                TurnAtObstacle();
                break;
            case "Pickup":
                PickUpItem(collision.gameObject);
                break;
            default:
                return;

        }

    }

    private void TurnAtObstacle()
    {
        _isTurning = true;
        bool prevWalkingState = _animator.GetBool(WALKING_ANIMATION_BOOL_NAME);
        _animator.SetBool(WALKING_ANIMATION_BOOL_NAME, false);
        LeanTween
            .rotateY(gameObject, _hasFlipped ? 0 : 180, speed / 10)
            .setOnComplete(() =>
            {
                _animator.SetBool(WALKING_ANIMATION_BOOL_NAME, prevWalkingState);
                _hasFlipped = !_hasFlipped;
                _isTurning = false;
            });
    }

    private void PickUpItem(GameObject pickupObject)
    {
        if ( pickupObject.transform.parent != transform)
        {
            pickupObject.transform.SetParent(transform);
            _animator.SetBool(HOLDING_ANIMATION_BOOL_NAME, true);
        }
    }

    private void DropItem()
    {
        GameObject pickedupItem = transform.GetComponentInChildren<PickupItem>().gameObject;
        if (pickedupItem != null) 
        {
            pickedupItem.transform.SetParent(null);
            pickedupItem.transform.position = transform.position + new Vector3(0, 0.4f, 0);
        }
    }

}
