using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManSpikes : MonoBehaviour
{
    float startTime;
    [SerializeField] float timeDelay;
    float timePercent;
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Vector2[] corners;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
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
