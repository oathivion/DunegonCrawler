using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class SnowmanDeath : MonoBehaviour
{
    SpriteRenderer selfSprite;
    public SpriteRenderer[] childSpriteRenders;
    HealthScript healthScript;
    [SerializeField] bool changeColorOnDeath;
    [SerializeField] GameObject exitDoor;
    // Start is called before the first frame update
    void Start()
    {
        selfSprite = gameObject.GetComponent<SpriteRenderer>();
        childSpriteRenders = transform.GetComponentsInChildren<SpriteRenderer>();
        healthScript = gameObject.GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript.GetDeath() && changeColorOnDeath)
        {
            
            changeColorOnDeath = false;
            Die();
        }
    }
    void Die()
    {
        exitDoor.SetActive(false);
        selfSprite.color = new Color(Color.red.r, Color.red.g, Color.red.b, .2f);
        foreach (SpriteRenderer sprite in childSpriteRenders)
        {
            sprite.color = selfSprite.color;
        }
        MonoBehaviour[] cs = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in cs)
        {
            c.enabled = false;
        }
        selfSprite.enabled = true;

    }
}
