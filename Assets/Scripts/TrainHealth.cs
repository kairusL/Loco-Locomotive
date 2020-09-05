﻿using UnityEngine;

public class TrainHealth : MonoBehaviour
{
    [SerializeField] private bool enableDebugLogging = false;
    public float trainHealth = 1000;
    public float currentHealth;
    private float restoreTime;
    private bool mIsTakingDamage;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = trainHealth;
        healthBar.SetMaxHealth(trainHealth);
    }

    public void TakeDamage(float amount)
    {
        if (IsAlive())
        {
            currentHealth -= amount;
            // currentHealth = Mathf.Max(currentHealth, 0f);
            if (enableDebugLogging)
            {
                Debug.Log("[Health] Lost " + amount + "hp. Current health: " + currentHealth);
            }
            healthBar.SetHealth(currentHealth);
        }

        if (IsAlive() == false)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void Restore()
    {
        if (currentHealth < trainHealth)
        {
            if (restoreTime > 20f)
            {
                currentHealth += 5;
                restoreTime = 0;
            }
            restoreTime += Time.deltaTime;

        }
    }
    public bool IsAlive()
    {
        if (currentHealth <= 0.0f)
        {
            return false;
        }
        return true;
    }
}
