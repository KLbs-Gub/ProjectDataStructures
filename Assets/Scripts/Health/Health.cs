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

    public float invincibleFrames = 0.0f;

    //events, can tie multiple things to that get triggered when the event is called
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHealed;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if (invincibleFrames > 0)
        {
            invincibleFrames--;
        }
    }

    public void TakeDamage(int amount)
    {
        if (invincibleFrames <= 0)
        {
            currentHealth -= amount;
            //calling event here
            OnPlayerDamaged?.Invoke();

            invincibleFrames = 55f;

            if (currentHealth <= 0)
            {
                //dead
                //gameoverscreen
                currentHealth = 0;
                Debug.Log("You Died");
                OnPlayerDeath?.Invoke();
            }
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
