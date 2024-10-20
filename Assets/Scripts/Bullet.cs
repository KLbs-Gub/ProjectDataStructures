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

    private Vector2 shotVector;

    // Gets and Sets

    public Vector2 ShotVector
    {
        get { return this.shotVector; }
        set { this.shotVector = value; }
    }

    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    // Methods

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = shotVector * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
