using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSpawner : MonoBehaviour
{

    public GameObject smokePrefab;
    public Transform smokePosition;


    void Start()
    {
        if (Random.value > 0.3f)
        {
            GameObject smokeEffect = Instantiate(smokePrefab, new Vector3 (smokePosition.transform.position.x - 0.1f, smokePosition.transform.position.y + 1f, smokePosition.transform.position.z), Quaternion.identity);

        }
    }

   
}
