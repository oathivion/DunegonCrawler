using System;
using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using UnityEngine;

// works on dynamic Rigidbody2D objects, activated by player movement & pressing "k"

// noted bug: objects launched in certain situations can lose interactivity temporarily or permanently

public class Kickable : MonoBehaviour
{
    private GameObject player; // to check what is applying force
    private bool isPlayerNearby = false;
    private Rigidbody2D rb;
    public float kickForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        // log own Rigidbody:
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // log current player:
        if (collision.collider.CompareTag("Player")) {
            player = collision.gameObject;
            isPlayerNearby = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        // unlog current player:
        if (collision.collider.CompareTag("Player")) {
            isPlayerNearby = false;
            player = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.K)) {
            KickSelf();
        }
    }

    void KickSelf() {
        if (player != null) { // player has to be nearby
            // note player's Rigidbody & velocity direction:
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            Vector2 playerDirection = playerRb.velocity.normalized;

            if (playerDirection.magnitude > 0) {
                // something about vectors i guess:
                rb.AddForce(playerDirection * kickForce, ForceMode2D.Impulse);
            }
            else {
                Debug.Log($"Player is at \"{gameObject.name}\" but not moving, cannot kick");
            }
        }
    }
}
