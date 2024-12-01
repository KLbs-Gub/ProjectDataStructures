//Written by Matthew Kopel
// 10/27/24
using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class ChaseEnemy : EnemyBase
{
    //Variables
    [HideInInspector] public GameObject target;
    public LayerMask mask;
    public float moveSpeed = 1f;

    private int direction = 1;
    private Rigidbody2D rb;
    private Vector2 moveDirection = new Vector2(0, 0);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(direction, 1);
    }

    private void FixedUpdate()
    {
        // trueTarget is the point of the actual hitbox
        // this is needed for line of sight checks to work.
        Vector3 trueTarget = new Vector3(target.transform.position.x, target.transform.position.y - 0.37f, target.transform.position.z);

        // From 'How to make Line of Sight in Unity 2D with Raycast' tutorial
        RaycastHit2D isBlocked = Physics2D.Raycast(this.transform.position, trueTarget - this.transform.position, 4000f, mask);

        if (isBlocked.collider != null)
        {
            if (isBlocked.collider.gameObject.CompareTag("Player"))
            {
                //takes position of player and always moves towards it
                transform.position = Vector2.MoveTowards(this.transform.position, trueTarget,
                    moveSpeed * Time.deltaTime);
                if (target.transform.position.x < transform.position.x)
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }
                Debug.DrawRay(this.transform.position, trueTarget - this.transform.position, Color.yellow);

                moveDirection.x = direction;
                moveDirection.y = -MathF.Sign((transform.position.y * transform.position.y) - (trueTarget.y * trueTarget.y));
            }
            else
            {
                rb.velocity = moveDirection * moveSpeed;
                Debug.DrawRay(this.transform.position, trueTarget - this.transform.position, Color.red);
            }
        }
    }

    public override void EnemyKilled()
    {
        Destroy(gameObject);
    }

}
