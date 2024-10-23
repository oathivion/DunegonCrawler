using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivation : MonoBehaviour
{
    [SerializeField] int stacks;
    [SerializeField] string damageType;
    [SerializeField] List<GameObject> validObjects;
    public DotScript dotScript;
    bool hidden;
    [SerializeField] Sprite cover;
    [SerializeField] Sprite unCovered;

    public void Start()
    {
        hidden = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = cover;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (validObjects.Contains(collision.gameObject))
        {
            if (hidden)
            {
                hidden = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = unCovered;
            }
            dotScript.InflictDoT(stacks, damageType, collision.gameObject);
        }
    }
}
