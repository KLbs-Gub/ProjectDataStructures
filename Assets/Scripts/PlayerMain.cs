// Written By Kieran Pounds
// 10/15/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    // Player Variables
    public float moveSpeed = 4.5f;

    // Directions: 0 = down, 1 = left, -1 = right, 2 = up
    private float direction = 0;

    private Rigidbody2D rb;
    private Animator animator;
    public Bullet bullet;

    private Vector2 movement;
    private Vector2 smoothedMovement;
    private Vector2 smoothedVelocity;

    //Gun Variables
    [Range(0.0f, 30.0f)][SerializeField] private float fireRate = 0.0f;
    private float shootTimer;
    private Vector2 shootDirection;

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
        //checks if any firing inputs are happening, then proceeds
        if (Input.GetAxisRaw("Fire1") != 0 && shootTimer <= 0f || 
            Input.GetAxisRaw("Fire2") != 0 && shootTimer <= 0f)
        {
            Shoot();
        }

        MovementInput();
        SpriteDirection();

        // Animation code
        transform.localScale = new Vector2(1, 1);
        if (movement == Vector2.zero) // If the player isn't moving
        {
            if (direction == 0) { animator.Play("IdleDown"); }
            if (direction == 1 || direction == -1)
            {
                animator.Play("IdleHori");
                transform.localScale = new Vector2(direction, 1);
            }
            if (direction == 2) { animator.Play("IdleUp"); }
        }
        else // If the player is moving
        {
            if (direction == 0) { animator.Play("WalkDown"); }
            if (direction == 1 || direction == -1)
            {
                animator.Play("WalkHori");
                transform.localScale = new Vector2(direction, 1);
            }
            if (direction == 2) { animator.Play("WalkUp"); }
        }
        
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

        movement = new Vector2(mx, my).normalized;
    }

  

    //angle checking for what sprites to use
    private void SpriteDirection()
    {
        if (Input.GetAxisRaw("Fire1") != 0 || Input.GetAxisRaw("Fire2") != 0)
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

        if (shootTimer <= 0)
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
    }
    private void ShootAngle()
    {
        float sx = Input.GetAxisRaw("Fire1");
        float sy = Input.GetAxisRaw("Fire2");

        if (sy < 0 && sx == 0)
        {
            direction = 0;
        }
        else if (sy > 0 && sx == 0)
        {
            direction = 2;
        }
        else if (sy == 0)
        {
            direction = sx * -1;
        }
    }
    private void Shoot()
    {
        if (shootDirection.y == 0)
        {
            shootDirection.x = Input.GetAxisRaw("Fire1");
        }
        if (shootDirection.x == 0)
        {
            shootDirection.y = Input.GetAxisRaw("Fire2");
        }

        if (shootDirection.x != 0 || shootDirection.y != 0)
        {
            if (shootTimer <= 0)
            {
                Vector2 shotOrigin = new Vector2(transform.position.x + shootDirection.x, (transform.position.y + shootDirection.y) - 0.15f);

                //for (int i = -1; i <= 1; i++)
                //{
                    Bullet shot = Instantiate(bullet, shotOrigin, transform.rotation);
                    shot.Speed = 11f;

                    //shootDirection.y = .5f * i;
                    // I add (smoothedMovement / 5) to transfer some movement velocity to the bullet
                    // we may need to tweak this later.
                    shot.ShotVector = shootDirection.normalized + (smoothedMovement / 5);
                    // Debug.Log("ShotVector: " + shot.ShotVector);
                //}

                shootTimer = 30 - fireRate;
            }
        }
    }
    
    private void killPlayer()
    {
        Destroy(gameObject);
    }


    private void OnEnable()
    {
        Health.OnPlayerDeath += killPlayer;
    }
    private void OnDisable()
    {
        Health.OnPlayerDeath -= killPlayer;
    }

}
