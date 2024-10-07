using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class Enemy : MonoBehaviour
{

    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if(healthComponent != null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakenDamage;
        }
    }

    private void TakenDamage(float health, float delta, float maxHealth)
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
}
