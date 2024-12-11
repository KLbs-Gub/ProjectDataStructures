// Written by Kieran Pounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropper : MonoBehaviour
{
    [SerializeField] private GameObject coin = null;
    [SerializeField] private GameObject heart = null;

    public void DropItem()
    {
        int randomChance = Random.Range(0, 100);
        if (randomChance <= 5)
        {
            GameObject pickup = Instantiate(heart, transform.position, transform.rotation);
        }
        else if (randomChance > 5 && randomChance <= 40)
        {
            GameObject pickup = Instantiate(coin, transform.position, transform.rotation);
        }
    }
}
