using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class Enemy : MonoBehaviour
{

    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComp;

    [SerializeField] BehaviourTree behaviourTree;

    
    // Start is called before the first frame update
    void Start()
    {
        if(healthComponent != null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakenDamage;
        }

        perceptionComp.onPerceptionTargetChanged += TargetChanged;
    }

    private void TargetChanged(GameObject target, bool sensed)
    {
      if (sensed)
        {
           behaviourTree.Blackboard.SetOrAddData("Target", target);
        }
        else
        {
            behaviourTree.Blackboard.RemoveBlackboardData("Target");
        }
    }

    private void TakenDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        
    }
    private void StartDeath()
    {
       TriggerDeathAnimation();
    }


    private void TriggerDeathAnimation()
    {
      if (animator != null)
        {
            animator.SetTrigger("Dead");
        }
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (behaviourTree && behaviourTree.Blackboard.GetBlackBoardData("Target", out GameObject target))
        {
            Vector3 drawTargetPos = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTargetPos, 0.7f);

            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPos);
        }
    }
}

