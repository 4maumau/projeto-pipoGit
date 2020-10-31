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

   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine("FollowTarget", player);
        positionY = Random.Range(5, 10);
    }

    IEnumerator FollowTarget(Transform target)
    {
        
        while (warningPhase > 0)
        {
            transform.position = new Vector2(target.position.x + warningOffSet, positionY); //Mathf.Lerp(transform.position.y, target.position.y, smoothing));
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

        GameObject Sock = Instantiate(sockPrefab, new Vector2 (transform.position.x + 1.5f, transform.position.y), Quaternion.identity);
        Destroy(gameObject);


        yield return null;
    }
}
