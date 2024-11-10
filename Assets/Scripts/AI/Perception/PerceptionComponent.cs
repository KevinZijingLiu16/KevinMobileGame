using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{

    [SerializeField] SenseComp[] senses;
    LinkedList<PerceptionStimuli> currentlyPerceivedStimuli = new LinkedList<PerceptionStimuli>();


    PerceptionStimuli targetStimuli;

    public delegate void OnPerceptionTargetChanged(GameObject target, bool sensed);
    public event OnPerceptionTargetChanged onPerceptionTargetChanged;



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
        var nodeFound = currentlyPerceivedStimuli.Find(stimuli);
        if (successfulySensed)
        {
            
            if (nodeFound != null)
            {
                currentlyPerceivedStimuli.AddAfter(nodeFound, stimuli);
            }
            else
            {
                currentlyPerceivedStimuli.AddLast(stimuli);
            }
        }
        else
        {
            currentlyPerceivedStimuli.Remove(nodeFound);
        }

        if(currentlyPerceivedStimuli.Count != 0)
        {
           PerceptionStimuli highestStimuli = currentlyPerceivedStimuli.First.Value;
            if(targetStimuli == null || targetStimuli != highestStimuli)
            {
                targetStimuli = highestStimuli;
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, true);
            }
        }
        else
        {
           if(targetStimuli != null)
            {
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, false);
                targetStimuli = null;
                
            }
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
