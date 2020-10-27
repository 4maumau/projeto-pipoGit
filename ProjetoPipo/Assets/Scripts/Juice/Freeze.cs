using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public float duration = 0.5f;
    private float freezeDuration = 0f;
    public bool isFrozen = false;

    private PlayerCollision playerCollision;

    void Start()
    {
        //playerCollision = FindObjectOfType<PlayerCollision>();
        //playerCollision.OnPlayerDeath += FreezeCall;
    }

    private void Update()
    {
        if (freezeDuration > 0 && !isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }

    public void FreezeCall()
    {
        freezeDuration = duration;
    }

    IEnumerator DoFreeze()
    {
        isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        freezeDuration = 0;
        isFrozen = false;
              
    }
}
