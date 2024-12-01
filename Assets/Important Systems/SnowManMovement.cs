using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManMovement : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startTime = Time.time;
        float moveTime = Random.Range(timeMin, timeMax);
        timeDelay = moveTime;

    }

    // Update is called once per frame
    [SerializeField] float speed;
    [SerializeField] float timeMin;
    [SerializeField] float timeMax;
    Vector2 direction;
    float startTime;
    float timeDelay;
    float timePercent;
    void Update()
    {
        timePercent = (Time.time - startTime) / timeDelay;
        if (timePercent >= 1)
        {
            startTime = Time.time;
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            float moveTime = Random.Range(timeMin, timeMax);
            timeDelay = moveTime;



        }
        else
        {
            transform.Translate(direction*speed*Time.deltaTime);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        direction = direction * -1;
    }
}
