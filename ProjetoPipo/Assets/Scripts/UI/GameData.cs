using System.Collections;
using Cinemachine;
using UnityEngine;
using static GameEvents;

public class GameData : MonoBehaviour
{

    // allows Game Objects to see what coins are collected, bu not change their value.
    public int coinsCollected = 0;

    public int overallCoins;
    public int CoinsCollected => coinsCollected;

    void Start()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            overallCoins = PlayerPrefs.GetInt("Coins");
        }
        else
        {

            PlayerPrefs.SetInt("Coins", overallCoins);
        }
        OnCoinGet += CollectCoin;
    }

    void CollectCoin(int coinValue)
    {
        coinsCollected += coinValue;
        TriggerCoinUpdate (coinsCollected);
    }

    private void OnDestroy()
    {
        overallCoins += coinsCollected;
        PlayerPrefs.SetInt("Coins", overallCoins);
        coinsCollected = 0;
    }

}
