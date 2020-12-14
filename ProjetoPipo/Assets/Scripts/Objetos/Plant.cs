using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private AudioClip[] bounceClips;
    [SerializeField] private AudioClip[] knockoffClips;
    [SerializeField] private AudioSource bounceSource;
    [SerializeField] private AudioSource knockSource;

    private bool hit = false;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            knockSource.clip = knockoffClips[Random.Range(0, knockoffClips.Length)];
            knockSource.Play();
            hit = true;
        }
        if (hit && collision.gameObject.layer == 10)
        {
            bounceSource.clip = bounceClips[Random.Range(0, bounceClips.Length)];
            bounceSource.Play();
        }
    }

}
