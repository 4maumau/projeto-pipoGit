using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;


public class MissileWarn : MonoBehaviour
{
    public Transform player;
    public float warningPhase = 3f;
    public float lastPhase = 1f;
    
    [SerializeField] private float warningOffSet = 10f;
    int positionY;

    [SerializeField] private GameObject sockPrefab = null;

    private Animator animator;
    [SerializeField] private AudioSource warningBeep;
    private AudioManager audioManager;

   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        player = GameObject.FindWithTag("Player").transform;
        
        StartCoroutine("FollowTarget", player);
        positionY = Random.Range(5, 10);
        

    }

    IEnumerator FollowTarget(Transform target)
    {
        
        while (warningPhase > 0)
        {
            transform.position = new Vector2(target.position.x + warningOffSet, positionY);
            warningPhase -= Time.deltaTime;
            yield return null;
        }

        while(lastPhase > 0)
        {
            animator.SetBool("lastPhase", true);
            
            transform.position = new Vector2(target.position.x + warningOffSet, transform.position.y);
            lastPhase -= Time.deltaTime;
            yield return null;
        }

        audioManager.PlaySound("FinalBeep");
        GameObject Sock = Instantiate(sockPrefab, new Vector2 (transform.position.x + 1.5f, transform.position.y), Quaternion.identity);
        Destroy(gameObject);


        yield return null;
    }

    public void PlayBeep(float pitch = 1f)
    {
        warningBeep.pitch = pitch;
        warningBeep.Play();
    }
}
