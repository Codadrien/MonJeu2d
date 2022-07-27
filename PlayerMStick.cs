using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMStick : MonoBehaviour
{

    public float speed;
    public float jumpForce;

    public Transform groundCheckTransform;
    public float groundCheckRadius;

    public Transform targetTransform;

    public float moveInput;
    public Rigidbody2D rb;
    public bool isGrounded;
    public bool bowCheck;

    public bool faceAnim;

    public Transform characterContainer;

    public float face;
    public Camera mainCamera;
    public Animator anim;

    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask mouseAimMask;

    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;
    public float velocity;

    public Joystick joystick;

    public BowScript bowScript;

    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Facing Rotation

        
        anim.SetFloat("speed", (rb.velocity.x) / speed);

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("bowCheck", bowCheck);

        if (isJumping == true && isGrounded == true)
        {

            anim.SetTrigger("jump");
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                
            }
        }

    }

    private void FixedUpdate()
    {
        // Movement
        moveInput = joystick.Horizontal;

        if (joystick.Horizontal >= .1f)
        {
            moveInput = 1;
        }
        else if (joystick.Horizontal <= -.1f)
        {
            moveInput = -1;
        }
        else
        {
            moveInput = 0f;
        }

        
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        
    }

    public void Jump()
    {
        if(isGrounded)
        {
            isJumping = true;
        }
      
    }
}