//Written by Matthew Kopel
// 10/27/24
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    //Variables
    public GameObject player;
    public float moveSpeed = 1f;
    public EnemyHealth hp;

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += enemyKilled;
    }
    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= enemyKilled;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //takes position of player and always moves towards it
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position,
            moveSpeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {

    }

    private void enemyKilled()
    {
        Destroy(gameObject);
    }

}
