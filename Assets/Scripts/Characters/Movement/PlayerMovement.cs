using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes user input and converts it to player movement.
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 40f;            // the full speed of the player
    [SerializeField] [Range(0, 1)] float cutJumpHeight = 0.5f;      // the amount to cut the jump height on button release

    CharacterController2D controller;       // reference to the character controller component on the player
    Rigidbody2D rb;                         // reference to the rigid body component on the player
    Animator animator;                      // reference to the animator component on the player

    // GENERAL

    float horisontalMove = 0f;              // the horisontal movement. left < 0, right > 0
    bool jump = false;                      // to check whether or not the player should jump
    bool crouch = false;                    // to check whether or not the player should crouch

    // JUMPING

    float jumpPressed = 0;                  // the time since the jump button has been pressed
    float jumpPressedTime = 0.2f;           // the time to reset the above parameter

    // GROUNDED

    float grounded = 0;                     // the time since the player left the ground
    float groundedTime = 0.25f;             // the time to reset the above parameter

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horisontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horisontalMove));

        grounded -= Time.deltaTime;
        if (controller.isGrounded())
        {
            grounded = groundedTime;
        }

        jumpPressed -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = jumpPressedTime;
        }

        if ((jumpPressed > 0) && (grounded > 0))
        {
            jumpPressed = 0;
            grounded = 0;
            jump = true;
            animator.SetBool("Jump", true);
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutJumpHeight);
            }
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    /// <summary>
    /// Responsible for resetting the jump bool in the animator.
    /// </summary>

    public void OnLanding()
    {
        if (grounded > 0)
        { return; }

        if (controller.isGrounded())
        {
            animator.SetBool("Jump", false);
        }
    }

    /// <summary>
    /// Responsible for telling the animator whether or not the player is crouching.
    /// </summary>
    /// <param name="isCrouching">whether or not the player is crouching</param>

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("Crouch", isCrouching);
    }

    /// <summary>
    /// Makes sure that player movement is frame indepedant.
    /// </summary>

    private void FixedUpdate()
    {
        controller.Move(horisontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
