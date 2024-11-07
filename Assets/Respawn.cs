using System;
using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform RespawnPoint;  // Drag your RespawnPoint GameObject here
    public int health;
    

    private void Start()
    {
        // Initialize player health and find RespawnPoint if not assigned in Inspector
        
        if (RespawnPoint == null)
        {
            RespawnPoint = GameObject.Find("RespawnPoint").transform;
        }
    }

    private void RespawnPlayer()
    {
        // Move the player to the respawn point and restore health
        transform.position = RespawnPoint.position;
    }

    
}
