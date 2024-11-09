using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    [SerializeField] private float radius = 5f;  // Default radius
    [SerializeField] private float damage = 10f; // Default damage
    [SerializeField] private List<string> targetTags = new List<string>(); // Tags to target
    public Collider2D[] collidersInRange;

    // Called to trigger explosion or AOE effect
    public void Explode()
    {
        // Use OverlapCircle to detect all colliders within the radius
        collidersInRange = Physics2D.OverlapCircleAll(transform.position, radius);
        Debug.Log($"{this.name} has exploded!");

        // Loop through all colliders found within the radius
        foreach (var collider in collidersInRange)
        {

            // Check if the object has a tag from the targetTags list
            if (targetTags.Contains(collider.tag))
            {
                // Try to get the HealthScript component and apply damage if it exists
                var healthScript = collider.GetComponent<HealthScript>();
                if (healthScript != null)
                {
                    healthScript.TakeDamage(damage);
                }
            }
        }
    }

    // Optionally visualize the radius in the editor for easier debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
