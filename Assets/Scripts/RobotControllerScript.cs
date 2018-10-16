using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControllerScript : MonoBehaviour
{

    public float maxSpeed = 10f;
    bool facingRight = true;

    private Rigidbody2D rb2d;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask WhatIsGround;
    public float jumpForce = 700;


	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, WhatIsGround);
        anim.SetBool("Ground", grounded);

        



        //Store the current horizontal input in the float moveHorizontal.
         float moveHorizontal = Input.GetAxis ("Horizontal");
 
         //Store the current vertical input in the float moveVertical.
         float moveVertical = Input.GetAxis ("Vertical");
 
         //Use the two store floats to create a new Vector2 variable movement.
         Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
 
         //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
         rb2d.AddForce (movement * maxSpeed);


        
        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));


        //Rigidbody2D.velocity = new Vector2(move * maxSpeed, Rigidbody2D.velocity.y);


       if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

	}

    void Update()
    {
        anim.SetFloat("vSpeed", rb2d.velocity.y);
        anim.SetBool("Grounded", false);
        if (grounded && Input.GetButtonDown("Jump"))
        {

            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
