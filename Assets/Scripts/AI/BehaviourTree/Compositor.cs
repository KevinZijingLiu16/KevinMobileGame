using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Compositor : BTNode
{
    //Composite is the collection of nodes.

     LinkedList<BTNode> children = new LinkedList<BTNode>();

    LinkedListNode<BTNode> currentChild = null;

    protected override NodeResult Execute()
    {
       if (children.Count == 0)
        {
            return NodeResult.Success;
        }
        currentChild = children.First;
        return NodeResult.InProgress;
    }

    protected BTNode GetCurrentChild()
    {
        return currentChild.Value;
    }

    protected bool Next()
    {
        if(currentChild != children.Last)
        {
            currentChild = currentChild.Next;
            return true;
        }
        return false;
    }

    protected override void End()
    {
        currentChild = null;
    }

}
