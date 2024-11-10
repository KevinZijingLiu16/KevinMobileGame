using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysAwareSense : SenseComp
{

    [SerializeField] float awareDistance = 3f;
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        return Vector3.Distance(transform.position, stimuli.transform.position) < awareDistance;
    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Gizmos.DrawWireSphere(transform.position + Vector3.up, awareDistance);
    }

}
