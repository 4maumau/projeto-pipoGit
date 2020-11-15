using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : MonoBehaviour
{
    public float moveSpeed;
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        
    }
}
