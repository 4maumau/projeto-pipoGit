using System.Collections;
using Cinemachine;
using UnityEngine;
using static GameEvents;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private GameObject destroyEffect;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerCoinGet(value);

            FindObjectOfType<AudioManager>().PlaySound("ItemPickup");
            GameObject CoinPickupVFX = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(CoinPickupVFX, 1f);
            Destroy(gameObject);
        }
    }
}
