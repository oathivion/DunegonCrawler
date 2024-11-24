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
    }

    // Update is called once per frame
    public bool bossTime;
    [SerializeField] float timeMin;
    [SerializeField] float timeMax;
    Vector2 direction;
    float startTime;
    float timeDelay;
    float timeComplete;
    void Update()
    {
        if (bossTime)
        {

            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            float moveTime = Random.Range(timeMin, timeMax);
            timeDelay = moveTime;


        }
    }

}
