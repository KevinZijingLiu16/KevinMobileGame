using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    BTNode Root;
    // Start is called before the first frame update
    void Start()
    {
        ConstructTree(out Root);
    }

    protected abstract void ConstructTree(out BTNode root);
    

    // Update is called once per frame
    void Update()
    {
        Root.UpdateNode();
    }
}
