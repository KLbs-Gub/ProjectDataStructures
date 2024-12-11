using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropper : MonoBehaviour
{
    [SerializeField] private GameObject coin = null;
    [SerializeField] private GameObject heart = null;

    public void DropItem()
    {
        GameObject pickup = Instantiate(heart, transform.position, transform.rotation);
    }
}
