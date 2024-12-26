using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Log : BTNode
{
    string message;
    public BTTask_Log(string message)
    {
        this.message = message;
    }

    protected override NodeResult Execute()
    {
        Debug.Log(message);
        return NodeResult.Success;
    }
}
