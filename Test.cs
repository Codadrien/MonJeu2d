using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
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


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Facing Rotation

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
            Debug.Log("Le raycast touche un objet");
            Debug.Log(hit.point);
            Debug.DrawRay(mainCamera.transform.position, Input.mousePosition, Color.red);
        }


        face = Mathf.Sign(targetTransform.position.x - transform.position.x);
        anim.SetFloat("speed", (rb.velocity.x) / speed);

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("bowCheck", bowCheck);
       
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {

            anim.SetTrigger("jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;

        }

        if (bowCheck == false && isGrounded == true && Input.GetKey(KeyCode.F))
        {
            
           bowCheck = true;
           anim.SetTrigger("bow");
           speed = 6;

        }
        if (bowCheck == true && isGrounded == true && Input.GetKey(KeyCode.G))
        {
            bowCheck = false;
            anim.SetTrigger("bow");
            speed = 8;
        }



    }

    private void FixedUpdate()
    {
        // Movement
        /*moveInput = joystick.Horizontal;

        if (joystick.Horizontal >= .2f)
        {
            moveInput = 1;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            moveInput = -1;
        }
        else
        {
            moveInput = 0f;
        }*/


        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        transform.localScale = new Vector3(face, 1, 1);

       

       
       
    }

}
