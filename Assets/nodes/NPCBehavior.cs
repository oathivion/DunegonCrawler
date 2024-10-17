using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    
    public BTNode myTree;
    // Start is called before the first frame update
    void Start()
    {
        // Create the behavior tree
        BTNode tree = new Selector(new List<BTNode>
        {
            new Sequence(new List<BTNode>
            {
                new ConditionNode(IsEnemyClose),
                new ActionNode(Attack)
            }),
            new ActionNode(Patrol)
        });

        // Execute the tree
        tree.Execute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
