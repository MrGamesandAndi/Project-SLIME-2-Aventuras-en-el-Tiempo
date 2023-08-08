using CodeMonkey;
using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFade : MonoBehaviour
{
    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = .6f;

    private Image barImage;
    private Image damagedBarImage;
    private float damagedHealthShrinkTimer;
    private HealthSystem healthSystem;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        damagedBarImage = transform.Find("DamageBar").GetComponent<Image>();
    }

    void Start()
    {
        healthSystem = new HealthSystem((int)(PlayerPrefs.GetFloat("PlayerCurrentHP") * 100));
        SetHealth(PlayerPrefs.GetFloat("PlayerCurrentHP"));
        damagedBarImage.fillAmount = barImage.fillAmount;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    public void Damage(int amount)
    {
        healthSystem.Damage(amount);
    }

    public void Heal(int amount)
    {
        healthSystem.Heal(amount);
    }

    public void Kill()
    {
        healthSystem.Kill();
    }

    private void Update()
    {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if (damagedHealthShrinkTimer < 0)
        {
            if (barImage.fillAmount < damagedBarImage.fillAmount)
            {
                float shrinkSpeed = 1f;
                damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized());
    }

    private void SetHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    }
}
