using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    public const float maxHealth = 165;
    public float currentHealth = maxHealth;
    public RectTransform healthBar;
    Rigidbody2D rigidbody2D;

    public Player player;

    public void Start()
    {
        rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        rigidbody2D.mass = currentHealth / maxHealth * 10;
        if (currentHealth == 0)
        {
            this.gameObject.GetComponent<Player>().Death();
        }
    }

    public float getHealth() {
        return currentHealth;
    }

    public void setHealth(float health)
    {
        this.currentHealth = health;
    }

    public void revive()
    {
        this.currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        StartCoroutine(wait());

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            this.gameObject.GetComponent<Player>().Death();
        }
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    IEnumerator wait()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
