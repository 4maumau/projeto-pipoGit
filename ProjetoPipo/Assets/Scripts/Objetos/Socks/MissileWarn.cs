using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissileWarn : MonoBehaviour
{
    public Transform player;
    [Range (0.01f, 0.1f)][SerializeField] private float smoothing = 0.048f;
    public float warningPhase = 3f;
    public float lastPhase = 1f;
    
    [SerializeField] private float warningOffSet = 10f;

    [SerializeField] private GameObject sockPrefab;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine("FollowTarget", player);
    }

   IEnumerator FollowTarget(Transform target)
    {
        while (warningPhase > 0)
        {
            transform.position = new Vector2(target.position.x + warningOffSet, Mathf.Lerp(transform.position.y, target.position.y, smoothing));
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
