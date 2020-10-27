using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    
    public static event Action<int> OnCoinGet;


    public static void TriggerCoinGet(int coinValue)
    {
        OnCoinGet?.Invoke(coinValue);
        Debug.Log("We got a coin! Worth $" + coinValue);
    }

    public static event Action<int> OnCoinUpdate;

    public static void TriggerCoinUpdate (int coinsCollected)
    {
        OnCoinUpdate?.Invoke(coinsCollected);
        Debug.Log("Updated Coin UI with " + coinsCollected + "coins.");
    }
}
