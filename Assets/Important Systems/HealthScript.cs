using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    [SerializeField] public float startingHealth; //Starting Health For the Character
    [SerializeField] private bool isPlayer;
    [SerializeField] private GameObject deadScreen;
    DeathManager deathManager;
    bool isDead;

    private float currentHealth; //Current Health for the Character

    void Start () {
        currentHealth = startingHealth; //Initializes Current Health
        isDead = false;
        if (isPlayer)
        {
            deathManager = GetComponent<DeathManager>();
        }
        
    }

    public void TakeDamage (float damageTaken) {
        currentHealth = currentHealth - damageTaken; //Subtracts damage equal to float input.

        if(currentHealth <= 0)
        {
            KillCharacter();
        }
    }

    private void KillCharacter() {
        isDead = true;
        if (isPlayer) {
            currentHealth = startingHealth;
            deathManager.CleanUp();
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }
    public bool GetDeath()
    {
        return isDead;
    }
}
