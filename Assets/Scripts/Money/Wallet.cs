using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int playerCash = 0;

    // Events
    public static event Action OnMoneyPickup;
    public static event Action OnMoneySpend;

    public void AddCash(int cashToAdd)
    {
        playerCash += cashToAdd;

        OnMoneyPickup?.Invoke();
    }

    public void RemoveCash(int cashToRemove)
    {
        playerCash -= cashToRemove;

        if (playerCash < 0)
        {
            playerCash = 0;
        }

        OnMoneySpend?.Invoke();
    }
}
