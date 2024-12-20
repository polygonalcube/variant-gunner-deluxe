using System;
using UnityEngine;

public class HPComponent : MonoBehaviour
{
    /// <summary>
    /// A component for storing health values.
    ///
    /// Stores health values for entities. Entities handle death logic.
    /// </summary>
    
    public int currentHealth;
    public int maxHealth;

    public bool healthStartsAtMax = true;

    private void Start()
    {
        if (healthStartsAtMax)
        {
            currentHealth = maxHealth;
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
    }
    
    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
    
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
