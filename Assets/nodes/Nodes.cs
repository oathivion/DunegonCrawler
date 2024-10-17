using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public abstract class BTNode
{
    public abstract bool Execute();
}

public class Selector : BTNode
{
    private List<BTNode> children;

    public Selector(List<BTNode> children)
    {
        this.children = children;
    }

    public override bool Execute()
    {
        foreach (var child in children)
        {
            if (child.Execute())
            {
                return true; // Succeeds if any child succeeds
            }
        }
        return false; // Fails if all children fail
    }
}

public class Sequence : BTNode
{
    private List<BTNode> children;

    public Sequence(List<BTNode> children)
    {
        this.children = children;
    }

    public override bool Execute()
    {
        foreach (var child in children)
        {
            if (!child.Execute())
            {
                return false; // Fails if any child fails
            }
        }
        return true; // Succeeds if all children succeed
    }
}

public class ActionNode : BTNode
{
    private Func<bool> action;

    public ActionNode(Func<bool> action)
    {
        this.action = action;
    }

    public override bool Execute()
    {
        return action(); // Perform the action and return true or false
    }
}

public class ConditionNode : BTNode
{
    private Func<bool> condition;

    public ConditionNode(Func<bool> condition)
    {
        this.condition = condition;
    }

    public override bool Execute()
    {
        return condition(); // Check the condition and return true or false
    }
}
