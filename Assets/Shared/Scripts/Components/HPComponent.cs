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

    public bool IsAlive()
    {
        return currenthealth > 0;
    }
    
    public bool IsDead()
    {
        return currenthealth <= 0;
    }
}
