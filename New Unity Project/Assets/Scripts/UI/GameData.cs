using System.Collections;
using Cinemachine;
using UnityEngine;
using static GameEvents;

public class GameData : MonoBehaviour
{

    // allows Game Objects to see what coins are collected, bu not change their value.
    public int coinsCollected = 0;

    //public CinemachineVirtualCamera vcam;
    //private bool zoomActive = false;
   // [SerializeField] private float zoomSpeed;
    public int CoinsCollected => coinsCollected;

    void Start()
    {
        OnCoinGet += CollectCoin;
    }

    void CollectCoin(int coinValue)
    {
        coinsCollected += coinValue;
        TriggerCoinUpdate (coinsCollected);
        //if (zoomActive)
       // {
         //   vcam.m_Lens.OrthographicSize = 4.8f;
         //   zoomActive = false;
       // }
      //  else if (!zoomActive)
       // {
      //      vcam.m_Lens.OrthographicSize = 4.5f;
      //      zoomActive = true;
       // }
    }

    private void OnDestroy()
    {
       
        coinsCollected = 0;
    }

    //private void LateUpdate()
   // {
        //if (zoomActive) vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, 4.5f, zoomSpeed);
    //}

}
