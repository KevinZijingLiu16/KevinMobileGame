using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeResult
{
    Success,
    Failure,
    InProgress
}
public abstract class BTNode 
{
   public NodeResult UpdateNode()
    {
        // one off thing

        if (!started)
        {
            started = true;
            NodeResult execResult = Execute();

            if (execResult != NodeResult.InProgress)
            {
                EndNode();
                return execResult;
            }
        }

        // time based thing

        NodeResult updateResult = Update();

        if (updateResult != NodeResult.InProgress)
        {
            EndNode();
        }

        return updateResult;
    }


    //override these methods in the derived class   


    protected virtual NodeResult Execute()
    {
        // one off thing
        return NodeResult.Success;
    }

   

    protected virtual NodeResult Update()
    {
        // time based thing
        return NodeResult.Success;
    }

    protected virtual void End()
    {
        // cleanup
    }

    private void EndNode()
    {
        started = false;
        End();
    }


    private bool started = false;
}
