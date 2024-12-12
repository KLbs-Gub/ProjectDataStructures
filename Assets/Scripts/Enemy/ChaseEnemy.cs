//Written by Matthew Kopel
// 10/27/24
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : EnemyBase
{
    //Variables
    [HideInInspector] public GameObject target;
    public LayerMask mask;
    public float moveSpeed = 1f;
    public bool hasSight = false;

    private int direction = 1;
    private Rigidbody2D rb;
    private PickupDropper dropper;
    private Vector2 moveDirection = new Vector2(0, 0);
    private float spawnTimer = 75f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dropper = GetComponent<PickupDropper>();
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

        // This region is based on 'How to make Line of Sight in Unity 2D with Raycast' tutorial
        #region LineOfSight
        // Line of sight stuff by Kieran Pounds
        RaycastHit2D isBlocked = Physics2D.Raycast(this.transform.position, trueTarget - this.transform.position, 4000f, mask);

        if (isBlocked.collider != null)
        {
            if (isBlocked.collider.gameObject.CompareTag("Player"))
            {
                hasSight = true;

                //takes position of player and always moves towards it - Matthew Kopel
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
                moveDirection.y = MathF.Sign(trueTarget.y - transform.position.y);
                rb.velocity = new Vector2(0.01f, 0.01f);
            }
            else
            {
                hasSight = false;
                rb.velocity = moveDirection * moveSpeed;
                
                // This prevents enemies from being invisible
                if (moveDirection.x != 0)
                {
                    direction = Convert.ToInt32(moveDirection.x);
                }
                Debug.DrawRay(this.transform.position, trueTarget - this.transform.position, Color.red);
            }
        }
        #endregion

        if (spawnTimer <= 0 && GetComponent<Collider2D>().enabled != true)
        {
            // Once the spawn timer is done, reenable collision and make sure that
            // the enemy moves towards its target if it doesn't immediately see it.
            GetComponent<Collider2D>().enabled = true;

            // Make enemy fully visible
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

            moveDirection.x = MathF.Sign(trueTarget.x - transform.position.x);
            moveDirection.y = MathF.Sign(trueTarget.y - transform.position.y);
        }
        else
        {
            spawnTimer--;
        }
    }

    public override void EnterRoom(string direction)
    {
        GetComponent<Collider2D>().enabled = false;

        // Make enemy semi-transparent while it is spawning
        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.7f);

        if (direction == "EntranceUp")
        {
            moveDirection = new Vector2(0, -1);
        }
        else if (direction == "EntranceDown")
        {
            moveDirection = new Vector2(0, 1);
        }
        else if (direction == "EntranceLeft")
        {
            moveDirection = new Vector2(1, 0);
        }
        else if (direction == "EntranceRight")
        {
            moveDirection = new Vector2(-1, 0);
        }
    }

    public override void EnemyKilled()
    {
        dropper.DropItem();
        Destroy(gameObject);
    }
}
