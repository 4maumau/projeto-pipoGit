using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameStarted = false;
    public bool alive = true;

    public Rigidbody2D rb;

    public Animator animator;
    public GameObject dustPrefab;  // for the dust animation
    public Transform legsPosition;

    private AudioManager audioManager;

    public float speed;
    public float jumpForce;

    public float hangTime = 0.1f;
    private float hangCounter;

    public float speedMultiplier;
    public float speedIncreaseMileStone;
    [SerializeField] private float speedMileStoneCount = 300f;


    public LayerMask whatIsGround;
    private BoxCollider2D boxCollider2d;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        // handling animator
        animator.SetBool("InGround", IsGrounded());
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("SpeedY", rb.velocity.y);
        animator.SetFloat("HangCounter", hangCounter);

        // speed increasing
        if (transform.position.x > speedMileStoneCount)
        {
            speedMileStoneCount += speedIncreaseMileStone;
            speed = speed * speedMultiplier;
            speedIncreaseMileStone = speedIncreaseMileStone * speedMultiplier;
        }

        // hang time
        if (IsGrounded())
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        
        //jump on mobile
        TouchJump();

        //jump on pc
        PCJump();
    }

    private void TouchJump()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && hangCounter > 0 && alive)
            {
                rb.velocity = Vector2.up * jumpForce;

                audioManager.PlaySound("PlayerJump");

                // create and destroy the dust effect
                GameObject DustEffect = Instantiate(dustPrefab, legsPosition.transform.position, Quaternion.identity);
                Destroy(DustEffect, 1f);
            }

        }

    }

    private void PCJump()
    {
        // jump on pc
        if (Input.GetButtonDown("Jump") && hangCounter > 0 && alive)
        {
            rb.velocity = Vector2.up * jumpForce;


            //jump sound
            audioManager.PlaySound("PlayerJump");
            // create and destroy the dust effect
            GameObject DustEffect = Instantiate(dustPrefab, legsPosition.transform.position, Quaternion.identity);
            Destroy(DustEffect, 1f);
        }
    }

    void FixedUpdate()
    {
        if (gameStarted == true) // to start running just after the first jump
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }

    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .4f, whatIsGround);
        return raycastHit.collider != null;
    }

    
}

