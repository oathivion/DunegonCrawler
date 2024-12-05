using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed; //The speed the projectile will go
    public string whoShouldIHitTag; //What Tag should I check for when compairing damage
    /// <summary>
    /// This doesn't work in my opinion if your going to use this kind of collision detection -Jordan
    /// </summary>
    public string whoShouldIIngnoreTag; //Who should I ignore.
    public LayerMask wallLayer;
    public float damage; //How Much Damage Should I hit
    public float despawnTime; //How long the projectile is live

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
        Debug.Log("Hit " + other.name);
        if (other.CompareTag(whoShouldIHitTag))
        {
            Debug.Log("Hit Player");
            if(TryGetComponent<HealthScript>(out HealthScript playerHealth)){
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }else if ((wallLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("Hit a wall");
            Destroy(gameObject);
        }
    }

    void Despawn()
    {
        Debug.Log("despawn");
        Destroy(gameObject);
    }
}
