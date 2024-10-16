using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class PlayerController : MonoBehaviour
    {
        //Public Variables
        public float baseSpeed;

        //Private Variables
        private StatsSaveSystem statsSaveSystem;
        private float modifiedSpeed;
        private Rigidbody2D rb;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            statsSaveSystem = FindObjectOfType<StatsSaveSystem>();
            rb = GetComponent<Rigidbody2D>();

            modifiedSpeed = baseSpeed + (int)statsSaveSystem.GetStat("dexterity");
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

            rb.velocity = modifiedSpeed * dir;
        }
    }