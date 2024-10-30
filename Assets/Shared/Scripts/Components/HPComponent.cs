using System;
using UnityEngine;

public class HPComponent : MonoBehaviour
{
    public int currenthealth;
    public int maxHealth;

    public bool healthStartsAtMax = true;

    private void Start()
    {
        if (healthStartsAtMax)
        {
            currenthealth = maxHealth;
        }
    }

    public void ChangeHealth(int amount)
    {
        currenthealth = Mathf.Clamp(currenthealth - amount, 0, maxHealth);
    }
    
    public void Heal(int amount)
    {
        currenthealth = Mathf.Clamp(currenthealth + amount, 0, maxHealth);
    }

    public bool IsAlive()
    {
        return currenthealth > 0;
    }
    
    public bool IsDead()
    {
        return currenthealth <= 0;
    }
}
