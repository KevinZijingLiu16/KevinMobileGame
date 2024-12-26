using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    BTNode Root;
    Blackboard blackbaard = new Blackboard();
    BTNode previousNode;

    public Blackboard Blackboard
    {
        get
        {
            return blackbaard;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ConstructTree(out Root);
        SortTree();
    }

    private void SortTree()
    {
       int priorityCounter = 0;
        Root.SortPriority(ref priorityCounter);
    }

    protected abstract void ConstructTree(out BTNode root);
    

    // Update is called once per frame

    void Update()
    {
        Root.UpdateNode();

        BTNode currentNode = Root.Get();

        if (previousNode != currentNode)
        {
            previousNode = currentNode;
            Debug.Log($"current node change to {currentNode}");
        }
    }

   
}
