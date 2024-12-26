using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_AlwaysFail : BTNode
{
    protected override NodeResult Execute()
    {
        Debug.Log("I always feel good");
        return NodeResult.Failure;
    }

  
}
