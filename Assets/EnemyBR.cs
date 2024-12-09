using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    public float speed;
    public float damage = 10f; // Damage dealt to the player
    public float knockbackForce = 5f; // Knockback force applied to the player
    public float enemyKnockbackForce = 3f; // Knockback force applied to the enemy
    public float damageInterval = 1f; // Time interval between each damage application (in seconds)

    private Rigidbody2D enemyRb;
    private bool isPlayerInRange = false;
    private float damageTimer = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyRb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D of the enemy
    }

    void Update()
    {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

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

            // Set flag to indicate player is within range
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // Set flag to indicate player is no longer in range
            isPlayerInRange = false;
            damageTimer = 0f; // Reset the damage timer when the player exits the trigger
        }
    }

    private void ApplyDamage()
    {
        // Apply damage to the player once per second
        HealthScript playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Apply damage
        }
    }
}
