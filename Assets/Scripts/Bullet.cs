// written by Matthew Kopel
// 10/15/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Variables
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;


    [Range(1, 10)]
    [SerializeField] private float lifeTime = 5f;

    private Vector2 movement;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        Debug.Log("Collided with: " + collision2D.name);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
