using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    NavMeshAgent agent;
    /// <summary>
    /// Player should be assigned automaticaly!!! this is temporary
    /// </summary>
    [SerializeField] GameObject player;
    HealthScript playerHealth;
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] bool stopWhileInRange = true;
    [Header("Attack")]
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float alertDistance = 6f;
    [SerializeField] float attackDistance = .9f;
    /// <summary>
    /// time in seconds between attacks
    /// </summary>
    [SerializeField] float attackSpeed = .5f;
    

    bool alert = true;
    Coroutine attackPatternRoutine;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.speed = moveSpeed;
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, alertDistance);
        if (hit.transform.gameObject.CompareTag("Player"))
        {
            Debug.Log("alerted!!");
            alert = true;
        }
        // Debug.Log(Vector2.Distance(transform.position, player.transform.position));
        if(Vector2.Distance(transform.position, player.transform.position) < attackDistance)
        {
            if(attackPatternRoutine == null)
            {
                attackPatternRoutine = StartCoroutine(attackPattern(attackDamage));
            }
            
        }
        else
        {
            if(attackPatternRoutine != null)
            {
                StopCoroutine(attackPatternRoutine);
                attackPatternRoutine = null;
            }
            
        }
        if (alert)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    void attack()
    {
        Debug.Log("Attacking Player");
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            
        }
    }

    IEnumerator attackPattern(float damage)
    {
        while (true)
        {
            attack();
            yield return new WaitForSeconds(attackSpeed);
        }
        
    }
}
