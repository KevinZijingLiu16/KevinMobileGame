using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_GetNextPatrolPoint : BTNode
{
    PatrollingComponent patrollingComponent;
    BehaviourTree tree;
    string patrolPointKey;
    public BTTask_GetNextPatrolPoint(BehaviourTree tree, string patrolPointKey)
    {
        patrollingComponent = tree.GetComponent<PatrollingComponent>();
        this.tree = tree;
        this.patrolPointKey = patrolPointKey;
    }

    protected override NodeResult Execute()
    {
       if(patrollingComponent!= null && patrollingComponent.GetNextPatrolPoint(out Vector3 point))
        {
           tree.Blackboard.SetOrAddData(patrolPointKey, point);
            return NodeResult.Success;
        }

        return NodeResult.Failure;

    }
}
