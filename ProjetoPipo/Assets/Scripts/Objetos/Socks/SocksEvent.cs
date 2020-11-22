using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SocksEvent : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    private ParticleSystem windParticle;
    [SerializeField]private float eventMilestone = 500;
    public Transform player;

    private int minSocks = 2;
    private int maxSocks = 3;
    public GameObject sockWarning;

    [SerializeField, Range(0f, 5f)] private float fadeDuration = 2f;

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
        audioManager.PlaySound("WindLoop", 0.65f, fadeDuration);

        yield return new WaitForSeconds(1);

        int numSocks = Random.Range(minSocks, maxSocks);
        for (int sock = 0; sock < numSocks; sock ++)
        {
            
            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
            
            Instantiate(sockWarning);
            yield return null;
            
        }
        minSocks += 1;
        maxSocks += 2;

        yield return new WaitForSeconds(3);
        windParticle.Stop();
        audioManager.StopSound("WindLoop", fadeDuration);
    }
}
