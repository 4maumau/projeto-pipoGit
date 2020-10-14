using System.Collections;
using Cinemachine;
using UnityEngine;
using static GameEvents;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private GameObject destroyEffect = null;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerCoinGet(value);

            FindObjectOfType<AudioManager>().PlaySound("ItemPickup");
            Destroy(gameObject);
        }
    }
}
