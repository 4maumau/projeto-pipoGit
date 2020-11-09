using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocksEvent : MonoBehaviour
{

    private ParticleSystem windParticle;
    [SerializeField]private float eventMilestone = 500;
    public Transform player;

    private int minSocks = 2;
    private int maxSocks = 3;
    public GameObject sockWarning;

    void Start()
    {
        windParticle = GetComponent<ParticleSystem>();
    }

   

    void Update()
    {
        if (player.position.x > eventMilestone) StartCoroutine("SockEvent");
    }


    IEnumerator SockEvent()
    {
        eventMilestone += eventMilestone * 1.05f;
        windParticle.Play();
        
        yield return new WaitForSeconds(2);

        int numSocks = Random.Range(minSocks, maxSocks);
        for (int sock = 0; sock < numSocks; sock ++)
        {
            
            yield return new WaitForSeconds(Random.Range(2f, 6f));
            
            Instantiate(sockWarning);
            yield return null;
            
        }
        minSocks += 1;
        maxSocks += 2;

        yield return new WaitForSeconds(8);
        windParticle.Stop();
    }
}
