//written by Matthew Kopel
// 10/26/24
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    //events, can tie multiple things to that get triggered when the event is called
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHealed;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        //calling event here
        OnPlayerDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            //dead
            //gameoverscreen
            currentHealth = 0;
            Debug.Log("You Died");
            OnPlayerDeath?.Invoke();
        }
    }
    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnPlayerHealed?.Invoke();
    }
}
