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
    public GameObject player;
    HealthScript playerHealth;
    [Header("Movement")]
    public float moveSpeed = 5f;
    public bool stopWhileInRange = true;
    [Header("Attack")]
    public float attackDamage = 10f;
    public float alertDistance = 6f;
    public float attackDistance = .9f;
    /// <summary>
    /// The layers that obstruct the enemies vision
    /// </summary>
    [SerializeField] LayerMask walls;
    /// <summary>
    /// time in seconds between attacks
    /// </summary>
    public float attackSpeed = .5f;
    

    bool alert = false;
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
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        dir.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distanceToPlayer, walls);
        
        if (!hit && distanceToPlayer < alertDistance)
        {
            // Debug.Log("alerted!!");
            alert = true;
        }

        // Debug.Log(Vector2.Distance(transform.position, player.transform.position));
        if(Vector2.Distance(transform.position, player.transform.position) < attackDistance)
        {
            if(attackPatternRoutine == null)
            {
                attackPatternRoutine = StartCoroutine(attackPattern());
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

    public virtual void attack()
    {
        Debug.Log("Attacking Player");
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            
        }
    }

    public virtual IEnumerator attackPattern()
    {
        while (true)
        {
            attack();
            yield return new WaitForSeconds(attackSpeed);
        }
        
    }
}
