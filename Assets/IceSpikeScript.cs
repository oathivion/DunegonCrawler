using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeScript : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] PolygonCollider2D polygon;
    [SerializeField] float damage;
    float startTime;
    [SerializeField] float timeDelay;
    float timePercent;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        timeDelay += particles.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (particles.IsAlive())
        {
            sprite.enabled = false;
            polygon.enabled = false;
        }
        else
        {
            sprite.enabled = true;
            polygon.enabled = true;
        }
        timePercent = (Time.time - startTime) / timeDelay;
        if (timePercent >= 1)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && sprite.enabled)
        {
            collision.gameObject.GetComponent<HealthScript>().TakeDamage(damage);
        }
    }
}
