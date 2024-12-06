using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Wait : BTNode
{
    float waitTime = 2f;
    float timeElapsed = 0f;

    public BTTask_Wait(float waitTime )
    {
        this.waitTime = waitTime;
    }
    protected override NodeResult Execute()
    {
        if(waitTime <= 0)
        {
            return NodeResult.Success;
        }
       //Debug.Log($"wait started with duration: {waitTime}");
        timeElapsed = 0f;
        return NodeResult.InProgress;
    }

    protected override NodeResult Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= waitTime)
        {
            //Debug.Log("wait ended");
            return NodeResult.Success;
           
        }
        //Debug.Log($"waiting... {timeElapsed}");
        return NodeResult.InProgress;
    }
}
