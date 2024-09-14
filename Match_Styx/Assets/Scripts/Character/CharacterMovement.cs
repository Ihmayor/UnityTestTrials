using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private float speed;
    private bool isRespawning;

    [SerializeField]
    private PlayerAsset _playerStats;

    private readonly float SLOWEST_SPEED = 0.5f;
    private readonly float NORMAL_SPEED = 10;
    private readonly float gravityValue = -9.81f;
    
    private Vector3 playerVelocity;
    private float _jumpHeight = 1.4f;

    void Awake()
    {
        speed = NORMAL_SPEED;
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
        Vector3 movement = new Vector3(hoz * speed, 0);
        if (isRespawning)
        {
            return;
        }


        if (Mathf.Abs(hoz) > 0 )
        {
            transform.eulerAngles = new Vector3(0, movement.x < 0 ? 180 : 0, 0);
            animator.SetBool("IsMoving", true);
            _playerStats.IsMoving = true;
            if (isRespawning)
                return;
            controller.SimpleMove(movement);
            playerVelocity.x = movement.x/2;
        }
        else if (movement.x == 0 && animator.GetBool("IsMoving"))
        {
            animator.SetBool("IsMoving", false);
            _playerStats.IsMoving = false;
            return;
        }
        
        //HandleJump();
    }

    private void HandleJump()
    {
        bool isGroundedPlayer = controller.isGrounded;
        if (isGroundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (isGroundedPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        // Apply the push
        body.velocity = pushDir * speed;
    }

    public void FreezeSpeed()
    {
        speed = SLOWEST_SPEED;
    }

    public void ThawSpeed()
    {
        speed = NORMAL_SPEED;
    }

    public void BoostSpeed()
    {
        StartCoroutine(UseBoost());
    }

    IEnumerator UseBoost()
    {
        float originalSpeed = speed;
        speed = NORMAL_SPEED/2;
        while (speed > originalSpeed)
        {
            speed -= 0.1f;
            yield return new WaitForSeconds(0.1f);
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
