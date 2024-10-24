using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed; //The speed the projectile will go
    [SerializeField] private string whoShouldIHitTag; //What Tag should I check for when compairing damage
    [SerializeField] private string whoShouldIIngnoreTag; //Who should I ignore.
    [SerializeField] private float damage; //How Much Damage Should I hit
    [SerializeField] private float despawnTime; //How long the projectile is live

    private Rigidbody2D projectileBody;
    private Vector2 shootDirection;


    void Awake()
    {
        projectileBody = GetComponent<Rigidbody2D>(); //Gets RigidBody Component
        Invoke("Despawn", despawnTime);
    }

    public void SetDirection(Vector2 direction)
    {
        shootDirection = direction.normalized;
        projectileBody.AddForce(shootDirection * projectileSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other) { //This Function Handles This Object Hitting Something Else
        if(other.CompareTag(whoShouldIHitTag)) {
            other.GetComponent<HealthScript>().TakeDamage(damage);
        }
        else if(other.CompareTag(whoShouldIIngnoreTag)) {
            return;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Despawn()
    {
        // Destroy the GameObject
        Destroy(gameObject);
    }
}
