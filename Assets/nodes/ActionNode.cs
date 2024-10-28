using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ActionNode : BaseNode {

	[Input] public int entry;
	[Output] public int exit;

    public override string getString()
    {
        return "Action";
    }
    public override NodeState Evalutate()
    {
        throw new System.NotImplementedException();
    }
}