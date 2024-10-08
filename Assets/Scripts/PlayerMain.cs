using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    private float moveSpeed = 5.25f;

    private Rigidbody2D rb;

    private Vector2 movement;
    private Vector2 smoothedMovement;
    private Vector2 smoothedVelocity;

    // Start and Awake are called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        MovementInput();
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

        movement = new Vector2(mx, my).normalized;
    }
}
