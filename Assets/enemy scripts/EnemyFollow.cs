using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Document Author: Allyn Crane - Enemy Design Team
//Note from Author: Feel free to edit, just leave comments so we can easily see who edited things and why.
//Editors:
//Resources used: https://www.youtube.com/watch?v=4zAN5QBwGt8
public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
