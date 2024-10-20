// Written By Kieran Pounds
// 10/15/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    // Player Variables
    private float moveSpeed = 5.25f;

    // Directions: 0 = down, 1 = left, -1 = right, 2 = up
    private float direction = 0;

    private float shootTimer = 0;

    private Rigidbody2D rb;
    private Animator animator;
    public Bullet bullet;

    private Vector2 movement;
    private Vector2 shootDirection;
    private Vector2 smoothedMovement;
    private Vector2 smoothedVelocity;

    // Methods

    // Start and Awake are called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Shoot();
        MovementInput();

        transform.localScale = new Vector2(1, 1);
        if (direction == 0) { animator.Play("IdleDown"); }
        if (direction == 1 || direction == -1) { 
            animator.Play("IdleHori");
            transform.localScale = new Vector2(direction, 1);
        }
        if (direction == 2) { animator.Play("IdleUp"); }
    }

    // FixedUpdate makes things consistent across framerates
    private void FixedUpdate()
    {
        // This makes the movement feel less abrupt
        smoothedMovement = Vector2.SmoothDamp(smoothedMovement, movement, ref smoothedVelocity, 0.075f);

        rb.velocity = smoothedMovement * moveSpeed;

        shootTimer--;
    }

    // Gets movement from inputs
    private void MovementInput()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

        if (shootDirection == Vector2.zero)
        {
            if (my < 0)
            {
                direction = 0;
            }
            else if (my > 0)
            {
                direction = 2;
            }
            else
            {
                direction = mx * -1;
            }
        }

        movement = new Vector2(mx, my).normalized;
    }

    private void Shoot()
    {
        if (shootDirection.y == 0)
        {
            shootDirection.x = Input.GetAxisRaw("Fire2");
            direction = -shootDirection.x;
        }
        if (shootDirection.x == 0)
        {
            shootDirection.y = Input.GetAxisRaw("Fire1");
            if (shootDirection.y == 1)
            {
                direction = 2;
            }
            else
            {
                direction = 0;
            }
        }

        if (shootDirection.x != 0 || shootDirection.y != 0)
        {
            if (shootTimer <= 0)
            {
                Vector2 shotOrigin = new Vector2(transform.position.x + shootDirection.x, (transform.position.y + shootDirection.y) - 0.25f);

                Bullet shot = Instantiate(bullet, shotOrigin, transform.rotation);
                shot.Speed = 11f;
                shot.ShotVector = shootDirection.normalized;

                shootTimer = 30;
            }
        }
    }


}
