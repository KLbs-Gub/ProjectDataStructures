// Edit of the bullet script by Matthew Kopel
// Edit done by Kieran Pounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Variables
    private float speed = 8f;
    private GameObject player;
    private Vector2 shotVector;
    private bool hasCollided = false;

    // Methods

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");

        Vector3 trueTarget = new Vector3(player.transform.position.x, player.transform.position.y - 0.37f, player.transform.position.z);

        transform.right = trueTarget - transform.position;
    }

    private void FixedUpdate()
    {
        transform.position += transform.right / speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hasCollided == false)
        {
            // Need this to prevent doing damage twice in one collision
            hasCollided = true;

            Destroy(gameObject);

            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
        else if (!collision.gameObject.CompareTag("Room"))
        {
            hasCollided = true;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}