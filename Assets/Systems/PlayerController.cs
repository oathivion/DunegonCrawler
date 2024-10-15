using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public string whatPlayerShouldILoad = "default";

        private StatsSaveSystem statsSaveSystem;
        private Rigidbody2D rb;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            statsSaveSystem = FindObjectOfType<StatsSaveSystem>();
            rb = GetComponent<Rigidbody2D>();
            LoadPlayer(); 
        }



        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement() {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            rb.velocity = speed * dir;
        }

        private void LoadPlayer()
        {
            if (statsSaveSystem == null)
            {
                Debug.LogError("StatsSaveSystem is not initialized! Make sure it is attached to a GameObject.");
                return;
            }

            if (statsSaveSystem.DoesSaveFileExist(whatPlayerShouldILoad))
            {
                PlayerStats loadedStats = statsSaveSystem.LoadPlayerStats(whatPlayerShouldILoad);
                if (loadedStats != null)
                {
                    // Apply loaded stats to the character
                    ApplyLoadedStats(loadedStats);
                    Debug.Log("Player stats loaded successfully.");
                }
                else
                {
                    Debug.LogWarning("Failed to load player stats.");
                }
            }
            else
            {
                Debug.LogWarning("Save file does not exist.");
                Debug.LogWarning("Either Make A New Player Or Create a New Player");
                CreatePlayer(); //Temp Function for Testing
            }
        }

        private void ApplyLoadedStats(PlayerStats stats)
        {
            // Apply loaded stats to the character
        }

        private void CreatePlayer() { //This is a temporary script for testing purposes. Player Creation will be moved to a different script in the future.
            PlayerStats playerStats = new PlayerStats {
                playerName = "default",
                maxHealth = 20,
                experience = 0,
                level = 0,
                strength = 10,
                dexterity = 10,
                constitution = 10,
                weaponOne = "sword",
                weaponTwo = "sword",
                armor = "none",
                statItemOne = "none",
                statItemTwo = "none",
                statItemThree = "none"
            };
    
            statsSaveSystem.SavePlayerStats(whatPlayerShouldILoad, playerStats);
            LoadPlayer();
        }


    }