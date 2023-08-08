using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    private int healthAmount;
    private int healthAmountMax = 100;

    public HealthSystem(int healthAmount)
    {
        this.healthAmount = healthAmount;
    }

    public void Damage(int amount)
    {
        healthAmount -= amount;
        if (healthAmount <= 0)
        {
            healthAmount = 0;
            Kill();
        }

        if(OnDamaged != null)
        {
            OnDamaged(this,EventArgs.Empty);
        }
    }

    public void Kill()
    {
        Loader.ReloadScene();
        PlayerPrefs.SetFloat("PlayerCurrentHP", 100);
    }

    public void Heal(int amount)
    {
        healthAmount += amount;

        if(healthAmount > healthAmountMax)
        {
            healthAmount = healthAmountMax;
        }

        if (OnHealed != null)
        {
            OnHealed(this, EventArgs.Empty);
        }
    }

    public float GetHealthNormalized()
    {
        float playerHp = (float)healthAmount / healthAmountMax;
        PlayerPrefs.SetFloat("PlayerCurrentHP", playerHp);
        return playerHp;
    }
}
