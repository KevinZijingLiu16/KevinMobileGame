using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void OnHealthChanged(float health, float delta, float maxHealth);
    public delegate void OnTakeDamage(float health, float delta, float maxHealth);
    public delegate void OnHealthEmpty();//Death

    [SerializeField] float health = 100f;
    [SerializeField] float maxHealth = 100f;



    public event OnHealthChanged onHealthChanged;
    public event OnTakeDamage onTakeDamage;
    public event OnHealthEmpty onHealthEmpty;

    public void ChangeHealth(float amt)
    {
        if (health == 0 || health == 0)
        {
            return;
        }

        health += amt;

        if (amt < 0)
        {
            onTakeDamage?.Invoke(health, amt, maxHealth);
        }

        onHealthChanged?.Invoke(health, amt, maxHealth);

        if (health <= 0)
        {
            health = 0;
            onHealthEmpty?.Invoke();
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }

        Debug.Log($"{gameObject}, taking damage: {amt} , health is now {health}");

    }
}
