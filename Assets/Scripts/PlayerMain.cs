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

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;
    private Vector2 shootDirection;
    private Vector2 smoothedMovement;
    private Vector2 smoothedVelocity;

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
    }

    // Gets movement from inputs
    private void MovementInput()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

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

        movement = new Vector2(mx, my).normalized;
    }
}
