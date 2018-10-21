using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotControllerScript : MonoBehaviour
{

    public float maxSpeed = 10f;
    public float maxAcceleration = 10f;
    public float moveForce = 365f;
    bool facingRight = true;

    private Rigidbody2D rb2d;
    Animator anim;

    [HideInInspector] public bool jump = false;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask WhatIsGround;
    public float jumpForce = 700;

    private int count;
    public Text countText;


	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        count = 0;
        SetCountText();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {


        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));
        
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, WhatIsGround);
        anim.SetBool("Ground", grounded);


        if (move * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * move * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);




       if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();


        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }


    }

    void Update()
    {

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }


        anim.SetFloat("vSpeed", rb2d.velocity.y);
        anim.SetBool("Grounded", false);
        if (grounded && Input.GetButtonDown("Jump"))
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);

            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Coins: " + count.ToString();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    

}
