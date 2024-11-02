using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] public float startingHealth; //Starting Health For the Character

     public float currentHealth; //Current Health for the Character

    void Start () {
        currentHealth = startingHealth; //Initializes Current Health
    }

    public void TakeDamage (float damageTaken) {
        currentHealth = currentHealth - damageTaken; //Subtracts damage equal to float input.

        if(currentHealth <= 0)
        {
            KillCharacter();
        }
    }

    private void KillCharacter() {
        //Placeholder Function
        Debug.Log(gameObject.name + "Is now Dead");
        return;
    }
}
