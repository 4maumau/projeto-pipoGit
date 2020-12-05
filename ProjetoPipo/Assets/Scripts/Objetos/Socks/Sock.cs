using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : MonoBehaviour
{
    private bool alive = true;
    public float moveSpeed;
    private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D jumpOnCollider;

    [SerializeField]
    private float deathPump = 20;
    [SerializeField] private float torqueSpeed = 500;
    [SerializeField] private float newGravity = 5f;

    private Animator animator;
    private AudioSource dedSFX;

    void Start()
    {
        //Destroy(gameObject, 5f);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dedSFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SockDed();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive) transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        
    }

    public void SockDed()
    {
        alive = false;
        animator.SetBool("IsAlive", alive);
        GetComponent<BoxCollider2D>().enabled = false;
        jumpOnCollider.enabled = false;

        rb.gravityScale = newGravity;
        rb.velocity = new Vector2(-deathPump * .7f, deathPump);
        rb.AddTorque(torqueSpeed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("We hit the children! Weird sentence though");
            dedSFX.Play();
            SockDed();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Playing sound");
            dedSFX.Play();
        }
    }
}
