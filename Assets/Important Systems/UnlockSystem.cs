using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSystem : MonoBehaviour
{
    //Set to the key that unlocks this objeect
    [SerializeField] GameObject linkedKey;
    bool lockedState = true;
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if the key object is a child of the object that collided with this object

        if (collision.gameObject.transform.Find(linkedKey.name))
        {
            lockedState = false;
            //Kills the key after getting unlocked
            Destroy(linkedKey);
        }
    }

    public void Update()
    {
        if (!lockedState)
        {
            //If unlocked, it turns off this object.
            gameObject.SetActive(false);
        }
    }





}
