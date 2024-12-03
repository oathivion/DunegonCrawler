using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public Variables
    public float baseSpeed;

    // Private Variables
    private StatsSaveSystem statsSaveSystem;
    private float modifiedSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        statsSaveSystem = FindObjectOfType<StatsSaveSystem>();
        rb = GetComponent<Rigidbody2D>();

        // Initialize speed
        UpdateSpeed();
    }

    private void Update()
    {
        // Update speed every frame to account for stat changes
        UpdateSpeed();
        HandleMovement();
    }

    private void UpdateSpeed()
    {
        if (statsSaveSystem != null)
        {
            object dexterityValue = statsSaveSystem.GetStat("dexterity");
            if (dexterityValue is int dexterity)
            {
                modifiedSpeed = baseSpeed + dexterity;
            }
            else
            {
                modifiedSpeed = baseSpeed;
            }
        }
        else
        {
            modifiedSpeed = baseSpeed;
        }
    }

    private void HandleMovement()
    {
        Vector2 dir = Vector2.zero;

        // Corrected movement directions
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1; // Left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1; // Right
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1; // Up
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1; // Down
        }

        dir.Normalize();

        // Apply velocity to the Rigidbody with corrected directions
        rb.velocity = modifiedSpeed * dir;
    }
}
