using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawn : MonoBehaviour
{
    public GameObject passaroPrefab;
    public Transform passaroPosition;



    private void Start()
    {
        if (Random.value > 0.9f) Instantiate(passaroPrefab, passaroPosition.transform.position, Quaternion.identity);
    }
}
