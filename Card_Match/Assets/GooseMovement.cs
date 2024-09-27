using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseMovement : MonoBehaviour
{

    [SerializeField]
    float speed = 0.1f;

    [SerializeField]
    private LineRenderer LineRenderer;
    [SerializeField]
    private Transform ReleasePosition;
    [Header("Display Controls")]
    [SerializeField]
    [Range(10, 100)]
    private int LinePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float TimeBetweenPoints = 0.1f;

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
            if (HasNoItems())
                _animator.SetBool(HOLDING_ANIMATION_BOOL_NAME, false);
            else
            {
                //Nullify any pushback received from the package tossed
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                SetMoving(true);
            }
        }
    }
    void ToggleMoving()
    {
        bool currentWalkingState = _animator.GetBool(WALKING_ANIMATION_BOOL_NAME);
        SetMoving(!currentWalkingState);
    }

    void SetMoving(bool _isMoving)
    {
        _animator.SetBool(WALKING_ANIMATION_BOOL_NAME, _isMoving);
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

    public void ThrowBelow()
    {
        DropItem(new Vector2(0, -5));
    }

    public void ThrowLeft()
    {
        DropItem(new Vector2(-250f, 600));
    }

    public void ThrowRight()
    {
        DropItem(new Vector2(250f, 600));
    }

    private void DropItem(Vector3 dropForce)
    {
        if (HasNoItems())
        {
            _animator.SetBool(HOLDING_ANIMATION_BOOL_NAME, false);
            return;
        }
        GameObject pickedupItem = transform.GetComponentInChildren<PickupItem>().gameObject;
        
        if (pickedupItem != null) 
        {
            pickedupItem.transform.SetParent(null);
            if (dropForce.y < 0)
            {
                pickedupItem.transform.position = gameObject.transform.position + dropForce;
            }
            else
            {
                //Add Force in the correct direction to toss the item
                Rigidbody2D pickedUpItemRb2d = pickedupItem.GetComponent<Rigidbody2D>();
                pickedUpItemRb2d.AddRelativeForce(dropForce);
            }
        }
    }

    private bool HasNoItems()
    {
        return transform.GetComponentInChildren<PickupItem>() == null;
    }


    IEnumerator ShowTrajectory(Vector3 DropForce, Rigidbody2D rb2dItem)
    {
        yield return new WaitForSeconds(0.1f);
        while (rb2dItem.velocity != Vector2.zero)
        {
            DrawProjection(rb2dItem.mass, DropForce);
            yield return null;
        }

        LineRenderer.enabled = false;
        yield return null;
    }

    private void DrawProjection(float mass, Vector3 dropForce)
    {
        LineRenderer.enabled = true;
        LineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
        Vector3 startPosition = ReleasePosition.position;
        Vector3 startVelocity = dropForce/ (mass * 50);
        int i = 0;
        LineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);
            LineRenderer.SetPosition(i, point);
        }   
    }

}
