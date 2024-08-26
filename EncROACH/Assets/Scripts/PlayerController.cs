using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    public float moveSpeed = 5;

    Rigidbody rigidBody;
    
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = GetDirection();
        MovePlayer(direction);
    }

    internal virtual Vector3 GetDirection()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(UpKey))
        {
            direction = Vector3.forward;
        }
        else if (Input.GetKey(DownKey))
        {
            direction = Vector3.back;
        }
        else if (Input.GetKey(LeftKey))
        {
            direction = Vector3.left;
        }
        else if (Input.GetKey(RightKey))
        {
            direction = Vector3.right;
        }
        return direction;
    }

    void MovePlayer(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            rigidBody.ResetCenterOfMass();
            rigidBody.MovePosition(rigidBody.position + moveSpeed * direction * Time.fixedDeltaTime);
        }
        direction = Vector3.zero;
    }
}
