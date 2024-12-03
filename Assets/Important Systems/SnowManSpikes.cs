using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManSpikes : MonoBehaviour
{
    float startTime;
    [SerializeField] float boxWidth;
    [SerializeField] float boxHeight;
    [SerializeField] float timeDelay;
    float timePercent;
    [SerializeField] GameObject objectToSpawn;
    Vector2[] corners = new Vector2[] {new Vector2(0,0), new Vector2(0,0)};
    GameObject player;
    Vector2 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        corners[0] = new Vector2(playerPosition.x - (boxWidth / 2), playerPosition.y - (boxHeight / 2));
        corners[1] = new Vector2(playerPosition.x + (boxWidth / 2), playerPosition.y + (boxHeight / 2));
        timePercent = (Time.time - startTime) / timeDelay;
        if (timePercent >= 1)
        {
            startTime = Time.time;
            float xPosition = Random.Range(corners[0].x, corners[1].x);
            float yPosition = Random.Range(corners[0].y, corners[1].y);
            Vector3 spawnPoint = new Vector3(xPosition, yPosition, 0);
            Instantiate(objectToSpawn, spawnPoint, transform.rotation);
        }
    }
}
