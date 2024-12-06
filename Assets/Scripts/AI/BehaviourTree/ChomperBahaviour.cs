using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBahaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        rootNode = new BTTask_Wait(2f);
    }

   
}
