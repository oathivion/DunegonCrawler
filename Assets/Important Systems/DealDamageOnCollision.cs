using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnCollision : MonoBehaviour
{
    HealthScript playerHealth;
    [SerializeField] float damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
