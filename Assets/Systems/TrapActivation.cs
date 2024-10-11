using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivation : MonoBehaviour
{
    [SerializeField] int stacks;
    [SerializeField] string damageType;
    [SerializeField] List<GameObject> validObjects;
    public DotScript dotScript;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (validObjects.Contains(collision.gameObject))
        {
            dotScript.InflictDoT(stacks, damageType, collision.gameObject);
        }
    }
}
