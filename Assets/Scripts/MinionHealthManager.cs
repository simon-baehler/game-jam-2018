using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHealthManager : MonoBehaviour {

    public const float maxHealth = 100f;
    public float currentHealth = maxHealth;
    public RectTransform healthBar;

    private void Update()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
