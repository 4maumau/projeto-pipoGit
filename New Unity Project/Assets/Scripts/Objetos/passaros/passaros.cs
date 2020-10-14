using Monobehaviours;
using System.Collections;
using UnityEngine;

public class passaros : MonoBehaviour
{
    private Animator animator;
    private bool playAnim = true;
    public string animationName;

    [SerializeField] private bool fly = false;
    private Rigidbody2D rb;
    private Vector2 velocity;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(8f, 7f);
                
    }

    void Update()
    {
        if (fly)
        {
            animator.SetBool("Fly", true);
            Destroy(gameObject, 1.5f);
        }
        if (playAnim && !fly)
            StartCoroutine(WaitAnim()); //wait random seconds for animation
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Player")
        {
            fly = true;
        }
    }

    public IEnumerator WaitAnim()
        {
            playAnim = false;
            float randomWait = Random.Range(0f, 1.5f);
            yield return new WaitForSeconds(randomWait);
            animator.Play(animationName); ;  //Put your animation string
            playAnim = true;
        }

    private void FixedUpdate()
    {
        if (fly) rb.MovePosition(rb.position + Vector2.Lerp(new Vector2 (0f, 0f), velocity, 1f) * Time.fixedDeltaTime);
    }
}
