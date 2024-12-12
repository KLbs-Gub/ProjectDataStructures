using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeep : MonoBehaviour
{
    public GameObject[] itemSpawnPoints;
    public GameObject[] shopItems;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < itemSpawnPoints.Length; i++)
            {
                GameObject item = Instantiate(shopItems[Random.Range(0, shopItems.Length)], 
                    itemSpawnPoints[i].transform.position, transform.rotation);
                item.transform.parent = transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.CompareTag("ShopItem"))
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
