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

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;
    private Vector2 smoothedMovement;
    private Vector2 smoothedVelocity;

    //Gun Variables
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer;
    private float shootDirection;


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
        SpriteDirection();
        //checks if any firing inputs are happening, then proceeds
        if (Input.GetAxisRaw("Fire1") != 0 && fireTimer <= 0f || 
            Input.GetAxisRaw("Fire2") != 0 && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }



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

        movement = new Vector2(mx, my).normalized;
    }

    //shooting code
    private void Shoot()
    {
        //finds angle for gun, interprets into degrees
        float rotateAngle = 0.0f;
        if (shootDirection == 0)
        {
            rotateAngle = 180;
        }
        else if (shootDirection == 2)
        {
            rotateAngle = 0;
        }
        else
        {
            rotateAngle = 90.0f * shootDirection;
        }
        //Wacky physics Quaternion
        //talk to Matthew Kopel (me) if you want rough breakdown
        Quaternion target = Quaternion.Euler(0, 0, rotateAngle);
        firingPoint.rotation = Quaternion.Slerp(firingPoint.rotation, target, 1);

        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }

    //angle checking for what sprites to use
    private void SpriteDirection()
    {
        if (Input.GetAxisRaw("Fire1") != 0 || Input.GetAxisRaw("Fire2") !=0)
        {
            ShootAngle();
        }
        else
        {
            MoveAngle();
        }
    }
    private void MoveAngle()
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
    }
    private void ShootAngle()
    {
        float sx = Input.GetAxisRaw("Fire1");
        float sy = Input.GetAxisRaw("Fire2");

        if (sy < 0)
        {
            direction = 0;
        }
        else if (sy > 0)
        {
            direction = 2;
        }
        else
        {
            direction = sx * -1;
        }
        shootDirection = direction;
    }
}
