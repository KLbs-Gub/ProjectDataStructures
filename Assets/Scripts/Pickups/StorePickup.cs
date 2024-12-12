// Written by Kieran Pounds

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StorePickup : MonoBehaviour
{
    public int price = 0;
    public GameObject textHolder;
    public string statToChange = "";
    public float statChangeAmount = 0.0f;

    private TextMeshPro priceTag;

    // Start is called before the first frame update
    void Start()
    {
        priceTag = textHolder.GetComponent<TextMeshPro>();

        if (priceTag != null)
        {
            priceTag.text = "$" + Convert.ToString(price);
        }
        else
        {
            Debug.LogError("PRICETAG IS NULL FOR " + this.name + "!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Wallet>().playerCash >= price)
            {
                collision.gameObject.GetComponent<Wallet>().RemoveCash(price);
                collision.gameObject.GetComponent<Player>().AlterStat(statToChange, statChangeAmount);
                Destroy(gameObject);
            }
        }
    }
}
