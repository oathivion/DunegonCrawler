using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    public float speed = 2f;
    public float damage = 10f; // Damage dealt to the player
    public float knockbackForce = 5f; // Knockback force applied to the player
    public float enemyKnockbackForce = 3f; // Knockback force applied to the enemy
    public float damageInterval = 1f; // Time interval between each damage application (in seconds)
    public float knockbackPauseDuration = 1f; // Duration for enemy to stop moving after knockback
    public float detectionRadius = 5f; // Radius within which the enemy can detect the player
    public float health = 50f; // Enemy's health

    private Rigidbody2D enemyRb;
    private bool isPlayerInRange = false;
    private bool isKnockedBack = false;
    private float damageTimer = 0f;
    private float knockbackTimer = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyRb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D of the enemy
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the enemy if health is 0 or below
            return;
        }

        // Check if the player is within the detection radius
        if (Vector2.Distance(transform.position, target.position) <= detectionRadius)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }

        // If the enemy is not knocked back and the player is in range, move towards the player
        if (!isKnockedBack && isPlayerInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (isKnockedBack)
        {
            // Increment knockback timer
            knockbackTimer += Time.deltaTime;
            if (knockbackTimer >= knockbackPauseDuration)
            {
                isKnockedBack = false; // Resume movement after pause duration
                knockbackTimer = 0f;
            }
        }

        // If the player is in range, check the damage timer and apply damage once per second
        if (isPlayerInRange)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                ApplyDamage();
                damageTimer = 0f; // Reset the damage timer
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // Apply knockback to the player
            Rigidbody2D playerRb = collider.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDirection = (collider.transform.position - transform.position).normalized;
                playerRb.velocity = knockbackDirection * knockbackForce;
            }

            // Apply knockback to the enemy using AddForce
            if (enemyRb != null)
            {
                Vector2 enemyKnockbackDirection = (transform.position - collider.transform.position).normalized;
                enemyRb.AddForce(enemyKnockbackDirection * enemyKnockbackForce, ForceMode2D.Impulse);
            }

            // Set the enemy to be knocked back and stop movement
            isKnockedBack = true;
            knockbackTimer = 0f; // Reset the knockback timer
        }
    }

    private void ApplyDamage()
    {
        // Apply damage to the player
        HealthScript playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Apply damage
        }
    }

    public void TakeDamage(float damageTaken)
    {
        // Reduce enemy's health
        health -= damageTaken;
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the enemy
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the editor for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
