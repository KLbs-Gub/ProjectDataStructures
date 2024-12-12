// Written by Kieran Pounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public EnemyBullet enemyBullet;

    private ChaseEnemy chaseLogic;
    private float shotTimer = 80f;

    // Start is called before the first frame update
    void Awake()
    {
        chaseLogic = GetComponent<ChaseEnemy>();
    }

    private void FixedUpdate()
    {
        if (chaseLogic != null)
        {
            if (chaseLogic.hasSight && shotTimer <= 0)
            {
                EnemyBullet shot = Instantiate(enemyBullet, transform.position, transform.rotation);
                shotTimer = 80f;
            }
        }

        if (shotTimer > 0)
        {
            shotTimer--;
        }
    }
}
