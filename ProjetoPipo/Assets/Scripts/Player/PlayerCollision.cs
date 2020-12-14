using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollision : MonoBehaviour
{
    public event Action OnPlayerDeath;
    private PlayerController playerController;


    // death animation
    private BoxCollider2D boxCollider2d;
    public float torqueSpeed;
    public float deathPump;
    public float sockPump;

    [SerializeField] private ParticleSystem deathStars = null;

    private Animator animator;
    private Rigidbody2D rb;
    
    [Range(0f, 1f)]public float shakeTime;
    [Range (0f, 10f)]public float shakeIntensity = 5f;

    //death sounds
    [SerializeField]private AudioSource deathImpactAudio = null;
    [SerializeField]private AudioSource deathStarsAudio = null;
    [SerializeField]private AudioSource deathMeowAudio = null;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Collider2D>().tag == "Death")
        {
            playerController.alive = false;
            
            StartCoroutine(CoroutineDeathAnimation());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.name == "jumpON")
        {

            if (this.transform.GetChild(0).transform.position.y > other.transform.position.y)
            {
                rb.velocity = Vector2.up * sockPump;
                
            }
            else
            {
                playerController.alive = false;

                StartCoroutine(CoroutineDeathAnimation());
            }
        }
    }
    IEnumerator CoroutineDeathAnimation()
    {
        animator.SetBool("Death", true);

        deathImpactAudio.Play();
        deathMeowAudio.Play();

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
        
        yield return new WaitForSeconds(shakeTime + .25f); // wait for shake to end + additional time
    
        animator.SetBool("Death", false);

        deathStars.Play();
        deathStarsAudio.Play();

        boxCollider2d.enabled = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = Vector2.up * deathPump;
        rb.AddTorque(torqueSpeed, ForceMode2D.Force);

        OnPlayerDeath?.Invoke();

        yield return new WaitForSeconds(1.5f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return null;
    }

    
}
