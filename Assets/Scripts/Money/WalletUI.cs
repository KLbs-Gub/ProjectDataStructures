// Written by Kieran Pounds

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    public Wallet playerWallet;

    private TextMeshProUGUI textUI;

    // Event stuff
    public void OnEnable()
    {
        Wallet.OnMoneyPickup += UpdateText;
        Wallet.OnMoneySpend += UpdateText;
    }

    public void OnDisable()
    {
        Wallet.OnMoneyPickup -= UpdateText;
        Wallet.OnMoneySpend -= UpdateText;
    }

    // Awake is called before the first frame update
    void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        if (textUI != null)
        {
            textUI.text = Convert.ToString(playerWallet.playerCash);
        }
    }
}
