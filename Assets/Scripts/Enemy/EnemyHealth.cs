//Written by Matthew Kopel
// 10/27/24
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 1f;
    public EnemyBase parentEnemy;

    //Enemy health function, made seperately so we could paste into other enemies
    public void EnemyDamaged(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            parentEnemy.EnemyKilled();
        }
    }
}
