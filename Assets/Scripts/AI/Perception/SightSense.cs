using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSense : SenseComp
{
    [SerializeField] float sightDistance = 5f;
    [SerializeField] float sightHalfAngle = 45f;
    [SerializeField] float eyeHeight = 1.5f;
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        float distance = Vector3.Distance(stimuli.transform.position, transform.position);
        if (distance > sightDistance)
            return false;

        Vector3 forwarDir = transform.forward;
        Vector3 stimuliDir = (stimuli.transform.position - transform.position).normalized;

       if( Vector3.Angle(forwarDir, stimuliDir) > sightHalfAngle)
            return false;

        if (Physics.Raycast(transform.position + Vector3.up*eyeHeight, stimuliDir, out RaycastHit hitInfo, sightDistance))

        {
            if (hitInfo.collider.gameObject != stimuli.gameObject)
            {
                return false;
            }
        }

       return true;

    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Vector3 drawCenter = transform.position + Vector3.up * eyeHeight;

        Gizmos.DrawWireSphere(drawCenter, sightDistance);

        Vector3 leftLimitDir = Quaternion.AngleAxis(sightHalfAngle, Vector3.up) * transform.forward;
        Vector3 rightLimitDir = Quaternion.AngleAxis(-sightHalfAngle, Vector3.up) * transform.forward;

        Gizmos.DrawLine(drawCenter, drawCenter + leftLimitDir * sightDistance);
        Gizmos.DrawLine(drawCenter, drawCenter + rightLimitDir * sightDistance);
    }
}
