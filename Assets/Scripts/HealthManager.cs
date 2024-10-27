//written by Matthew Kopel
// 10/26/24
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Health playerHealth;
    List<HealthHeartChange> hearts = new List<HealthHeartChange>();

    //OnPlayerDamaged calls this, then makes it call DrawHearts
    private void OnEnable()
    {
        Health.OnPlayerDamaged += DrawHearts;
    }
    //calls it to shut it off
    private void OnDisable()
    {
        Health.OnPlayerDamaged -= DrawHearts;
    }

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();
        int heartsToMake = playerHealth.maxHealth;

        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            //takes current hp, - hearts made, then forces the num to be either 1 or 0, whichever is closer
            int heartStatusRemainder = Mathf.Clamp(playerHealth.currentHealth - i, 0, 1);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }

    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeartChange heartComponent = newHeart.GetComponent<HealthHeartChange>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeartChange>();
    }


}
