using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BTTask_MoveToTarget : BTNode
{
    NavMeshAgent agent;

    string targetKey;
    GameObject target;
    float acceptableDistance = 1.0f;
    BehaviourTree tree;

    public BTTask_MoveToTarget(BehaviourTree tree, string targetKey, float acceptableDistance = 1f)
    {
       this.targetKey = targetKey;
        this.acceptableDistance = acceptableDistance;
        this.tree = tree;
    }

    protected override NodeResult Execute()
    {
        Blackboard blackboard = tree.Blackboard;
        if(blackboard == null || !blackboard.GetBlackBoardData(targetKey, out target))
        {
            return NodeResult.Failure;
        }

        agent = tree.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            return NodeResult.Failure;
        }

        if(IsTargetInAcceptableDistance())
        {
            return NodeResult.Success;
        }

        blackboard.onBlackboardValueChanged += OnBlackboardValueChanged;

        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
        return NodeResult.InProgress;
    }

    private void OnBlackboardValueChanged(string key, object value)
    {
        if (key == targetKey)
        {
            target = (GameObject)value;
        }
    }

    protected override NodeResult Update()
    {
       if(target == null)
        {
          agent.isStopped = true;
            return NodeResult.Failure;
        }

       agent.SetDestination(target.transform.position);

        if (IsTargetInAcceptableDistance())
        {
            agent.isStopped = true;
            return NodeResult.Success;
        }

        return NodeResult.InProgress;


    }

    bool IsTargetInAcceptableDistance()
    {
        return Vector3.Distance(target.transform.position, tree.transform.position) <= acceptableDistance;
    }


}
