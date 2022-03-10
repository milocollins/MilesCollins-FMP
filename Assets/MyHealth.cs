using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHealth : HealthManager
{
    public float maxHealth = 100f;
    public float health = 100f;
    public float cooldownTime = 4f;
    private float cooldownTimer;
    private bool cooldownActive;
    public float recoveryRate = 4f;

    private void Awake()
    {
        health = maxHealth;
        cooldownActive = false;
    }
    private void Update()
    {
        if (health < maxHealth)
        {
            if (!cooldownActive)
            {
                health += recoveryRate * Time.deltaTime;
                UIManager.instance.UpdateHealth(health);
                if (health >= maxHealth)
                {
                    health = maxHealth;
                    UIManager.instance.ToggleHealthBar(false);
                }
            }
            else
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0)
                {
                    cooldownActive = false;
                }
            }
        }
    }
    public override void TakeDamage(Vector3 location, Vector3 direction, float damage, Collider bodyPart = null, GameObject origin = null)
    {
        health -= damage;
        if (health < 0 && !dead)
        {
            Kill();
        }
        else
        {
            if (!UIManager.instance.healthBar.activeInHierarchy)
            {
                UIManager.instance.ToggleHealthBar(true);
            }
            UIManager.instance.UpdateHealth(health);
            cooldownActive = true;
            cooldownTimer = cooldownTime;
            Debug.Log(health);
        }
    }
    public void Kill()
    {
        dead = true;
        health = 0;
        Debug.Log("Dead");
        if (UIManager.instance.deathScreen == null)
        {
            GameManager.instance.ChangeScene("Prototype");
        }
        else
        {
            UIManager.instance.DeathScreen();
        }
    }
    
}
