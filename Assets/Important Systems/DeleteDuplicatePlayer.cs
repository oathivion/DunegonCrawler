using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDublicatePlayer : MonoBehaviour
{
    private GameObject[] taggedObjects;

    void Start()
    {
        // Find all objects with the specified tag
        taggedObjects = GameObject.FindGameObjectsWithTag("Player");

        if (taggedObjects[1] != null) {
            Destroy(taggedObjects[1]);
        }
    }
}
