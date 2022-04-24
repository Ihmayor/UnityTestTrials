using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;
    Camera viewCamera;

    Vector3 velocity;

    public float moveSpeed = 6;

    public BoolVariable isGameWon;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
        originalPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameWon.Value)
            return;

        Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
        transform.LookAt(mousePos+ Vector3.up * transform.position.y);
        velocity = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical")).normalized * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (isGameWon.Value)
            return;

        rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGameWon.Value)
            return;

        if (collision.gameObject.name.Contains("Ghost"))
           transform.position = originalPosition;
    }

}
