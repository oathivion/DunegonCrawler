using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathToTarget : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    Transform target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
          
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
