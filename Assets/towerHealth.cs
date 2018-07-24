using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class towerHealth : MonoBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthBar;
    private void Update()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }
}
