using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingComponent : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoint;
    int currentPatrolPointIndex = -1;

    public bool GetNextPatrolPoint(out Vector3 point)
    {
            point = Vector3.zero;
        if(patrolPoint.Length == 0)
        {
            return false;
        }
        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoint.Length;

        point = patrolPoint[currentPatrolPointIndex].position;
        return true;
    }
}
