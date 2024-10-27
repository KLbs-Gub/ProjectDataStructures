//Written by Matthew Kopel
// 10/27/24
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 1f;
    public static event Action OnEnemyDeath;
    
    //Enemy health function, made seperately so we could paste into other enemies
    public void EnemyDamaged(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Debug.Log("Enemy Killed");
            OnEnemyDeath?.Invoke();
        }
    }
}
