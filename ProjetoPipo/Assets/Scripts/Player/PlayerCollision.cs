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
    [SerializeField] private ParticleSystem deathStars = null;

    private Animator animator;
    private Rigidbody2D rb;
    
    [Range(0f, 1f)]public float shakeTime;
    [Range (0f, 10f)]public float shakeIntensity = 5f;

    //death sounds
    [SerializeField]private AudioSource deathImpactAudio = null;
    [SerializeField]private AudioSource deathStarsAudio = null;

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

     IEnumerator CoroutineDeathAnimation()
    {
        animator.SetBool("Death", true);

        deathImpactAudio.Play();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
        yield return new WaitForSeconds(shakeTime + .25f); // wait for shake to end + additional time
    
    
        animator.SetBool("Death", false);
        boxCollider2d.enabled = false;
        deathStars.Play();
        deathStarsAudio.Play();
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = Vector2.up * deathPump;
        rb.AddTorque(torqueSpeed, ForceMode2D.Force);

        OnPlayerDeath?.Invoke();

        yield return null;
    }

    
}
