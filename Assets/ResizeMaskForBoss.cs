using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeMaskForBoss : MonoBehaviour
{
    [SerializeField] GameObject bossTriger;

    public void OnTriggerEnter(Collider collision)
    {
        print("Im getting triggerd");
    }

    /*
    {
        
        if (collision.gameObject.Equals(bossTriger))
        {
            Debug.Log("Entered The BOOOSSS ROOOMM");
            transform.localScale = new Vector2(30, 30);
        }
    }
    */
}
