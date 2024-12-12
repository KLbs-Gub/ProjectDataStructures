// Written by Kieran Pounds

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreAlert : MonoBehaviour
{
    public WaveManager wm;

    private float alertTimer = 0.0f;
    private TextMeshProUGUI textUI;

    // Event stuff
    public void OnEnable()
    {
        WaveManager.OnWaveComplete += ShowAlert;
    }
    public void OnDisable()
    {
        WaveManager.OnWaveComplete -= ShowAlert;
    }

    // Awake is called before the first frame update
    void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        if (alertTimer > 0.0f)
        {
            alertTimer--;
        }
        else if (alertTimer <= 0.0f && textUI.enabled == true)
        {
            textUI.enabled = false;
        }
    }

    // Method
    public void ShowAlert()
    {
        textUI.text = "Store is: " + LocationCompare();
        textUI.enabled = true;
        alertTimer = 80.0f;
    }

    private string LocationCompare()
    {
        string message = "";

        Player player = FindFirstObjectByType<Player>();
        GameObject shop = GameObject.Find("ShopKeep");

        if (shop.transform.position.y > player.currentRoomPosition.y)
        {
            message += "North";
        }
        else if (shop.transform.position.y < player.currentRoomPosition.y)
        {
            message += "South";
        }

        if (shop.transform.position.x < player.currentRoomPosition.x)
        {
            message += "West";
        }
        else if (shop.transform.position.x > player.currentRoomPosition.x)
        {
            message += "East";
        }

        return message;
    }
}
