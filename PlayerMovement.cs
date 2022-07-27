using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public float moveInput;
    public Animator anim;
    public Transform characterContainer;
    public int side;
    public bool facingRight;

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    public Vector3 direction;
    public Vector3 positionJoueur;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    }

    void Update()
    {
        Vector3 mousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionJoueur = transform.position;
        direction = mousPosition - positionJoueur;


        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        anim.SetFloat("speed", (rb.velocity.x) / speed);
        anim.SetBool("isGrounded", isGrounded);

        if (moveInput > 0 || direction.x > 0)
        {
            side = 1;
            Vector3 theScale = characterContainer.localScale;
            if (facingRight)
            {
                theScale.x *= -1;
                characterContainer.localScale = theScale;
                facingRight = false;
            }

        }

        if (moveInput < 0 || direction.x < 0) 
        {
            side = -1;
            Vector3 theScale = characterContainer.localScale;
            if (!facingRight)
            {
                theScale.x *= -1;
                characterContainer.localScale = theScale;
                facingRight = true;
            }
        }

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

    }

}


