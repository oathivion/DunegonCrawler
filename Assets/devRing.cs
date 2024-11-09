using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devRing : MonoBehaviour
{

    [SerializeField] private AOE aoe;

    void Start () {
        aoe = GetComponent<AOE>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("I SAID EXPLOAD!");
            aoe.Explode();
        }
    }
}
