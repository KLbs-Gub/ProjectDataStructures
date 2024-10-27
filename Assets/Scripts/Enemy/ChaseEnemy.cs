//Written by Matthew Kopel
// 10/27/24
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    //Variables
    [HideInInspector] public GameObject target;
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
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //takes position of player and always moves towards it
        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position,
            moveSpeed * Time.deltaTime);
    }

    private void enemyKilled()
    {
        Destroy(gameObject);
    }

}
