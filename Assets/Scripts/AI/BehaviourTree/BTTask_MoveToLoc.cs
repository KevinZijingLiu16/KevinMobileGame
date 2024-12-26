using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveToLoc : BTNode
{


    NavMeshAgent agent;

    string locKey;
    Vector3 loc;
    float acceptableDistance = 1.0f;
    BehaviourTree tree;

    public BTTask_MoveToLoc(BehaviourTree tree, string locKey, float acceptableDistance = 1f)
    {
        this.locKey = locKey;
        this.acceptableDistance = acceptableDistance;
        this.tree = tree;
    }


    protected override NodeResult Execute()
    {
        Blackboard blackboard = tree.Blackboard;

        if (blackboard == null || !blackboard.GetBlackBoardData(locKey, out loc))
        {
            return NodeResult.Failure;
        }

        agent = tree.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            return NodeResult.Failure;
        }

        if (IsLocInAcceptableDistance())
        {
            return NodeResult.Success;
        }

        agent.SetDestination(loc);
        agent.isStopped = false;
        return NodeResult.InProgress;

    }

    protected override NodeResult Update()
    {
        if (IsLocInAcceptableDistance())
        {
            agent.isStopped = true;
            return NodeResult.Success;
        }

        return NodeResult.InProgress;
    }

    private bool IsLocInAcceptableDistance()
    {
       return Vector3.Distance(loc,tree.transform.position) <= acceptableDistance;
    }
}
