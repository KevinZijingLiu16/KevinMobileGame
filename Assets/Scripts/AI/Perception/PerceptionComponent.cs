using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{

    [SerializeField] SenseComp[] senses;
    // Start is called before the first frame update
    void Start()
    {
        foreach (SenseComp sense in senses)
        {
            sense.onPerceptionUpdated += SenseUpdated;
        }
    }

    private void SenseUpdated(PerceptionStimuli stimuli, bool successfulySensed)
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
