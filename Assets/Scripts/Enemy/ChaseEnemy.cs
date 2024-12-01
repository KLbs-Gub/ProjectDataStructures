//Written by Matthew Kopel
// 10/27/24
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
                Debug.DrawRay(this.transform.position, trueTarget - this.transform.position, Color.yellow);
            }
            else
            {
                Debug.DrawRay(this.transform.position, trueTarget - this.transform.position, Color.red);
            }
        }
    }

    public override void EnemyKilled()
    {
        Destroy(gameObject);
    }

}
