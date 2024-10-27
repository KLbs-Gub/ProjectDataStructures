//Written by Matthew Kopel
// 10/27/24
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : EnemyBase
{
    //Variables
    [HideInInspector] public GameObject target;
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void FixedUpdate()
    {
        //takes position of player and always moves towards it
        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position,
            moveSpeed * Time.deltaTime);
    }

    public override void EnemyKilled()
    {
        Destroy(gameObject);
    }

}
