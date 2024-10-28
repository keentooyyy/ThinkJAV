using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;           // Speed of player movement
    public float jumpForce = 10f;      // Force applied for jump
    public float fallMultiplier = 2.5f; 

    private Rigidbody2D rb;            // Reference to Rigidbody2D component
    private bool isGrounded = true;    // Check if player is on the ground

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        // Check for left and right arrow key inputs
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            // Stop horizontal movement when no arrow key is pressed
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Jump()
    {
        // Check if the space key is pressed and player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    void FasterFalling()
    {
        // Apply faster falling effect when player is falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    // Detect if the player is on the ground through collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
