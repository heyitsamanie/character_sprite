using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RobotControllerScript : MonoBehaviour
{

    public float maxSpeed = 10f;
    public float maxAcceleration = 10f;
    public float moveForce = 365f;
    bool facingRight = true;

    private Rigidbody2D rb2d;
    Animator anim;

    [SerializeField]
    private PhysicsMaterial2D RunningPhysicsMaterial, StoppingPhysicsMaterial;

    [SerializeField]
    private Collider2D playerGroundCollider;

    [HideInInspector] public bool jump = false;

    private bool grounded = false;
    private bool doubleJump = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask WhatIsGround;
    public float jumpForce = 700;
    public float jumpVelocity = 7;

    private int count;
    public Text countText;

    private Checkpoint currentCheckpoint;

    private bool isDead = false;

    private AudioSource coinSound;


	// Use this for initialization
	private void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coinSound = GetComponent<AudioSource>();

        count = 0;
        SetCountText();
    }
	
	// Update is called once per frame
	private void FixedUpdate ()
    {
        UpdatePhysicsMaterial();

        if (!isDead)

        Move();
    }

    private void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");

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
    }

    void Update()
    {
        CheckIfGrounded();
        CheckRespawn();
        UpdateAnimationPerameters();

        if (!isDead)
            Jump();
    }

    private void CheckIfGrounded()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void UpdateAnimationPerameters()
    {
        anim.SetFloat("vSpeed", rb2d.velocity.y);
        anim.SetBool("Grounded", false);
        anim.SetBool("isDead", isDead);
    }

    private void Jump()
    {
        if (grounded && Input.GetButtonDown("Jump"))
        {
            doubleJump = false;
            rb2d.AddForce(new Vector2(0, jumpForce));
            anim.SetBool("Jump", jump);
        }

        if (Input.GetButtonDown("Jump") && grounded && doubleJump)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }

        if (Input.GetButtonDown("Jump") && !grounded && !doubleJump)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
            doubleJump = true;
        }
        
    }

    private void UpdatePhysicsMaterial()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            playerGroundCollider.sharedMaterial = RunningPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = StoppingPhysicsMaterial;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            coinSound.Play();

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

    public void CheckRespawn()
    {
        if (isDead && Input.GetButtonDown("Respawn"))
        {
            if (currentCheckpoint == null)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            else
            {
                anim.Play("Idle");
                rb2d.velocity = Vector2.zero;
                transform.position = currentCheckpoint.transform.position;
                isDead = false;
            }
        } 
    }

    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.SetIsActivated(false);

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }
    
    public void Die()
    {
        isDead = true;
    }

}
