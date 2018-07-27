using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthManager : NetworkBehaviour {

    public const float maxHealth = 100f;

    [SyncVar]
    public float currentHealth = maxHealth;

    public RectTransform healthBar;
    private AudioSource TowerAudio;

    private void Awake()
    {
        TowerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    public void TakeDamage(float amount)
    {
        if(!isServer)
        {
            return;
        }

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
        TowerAudio.Play();
        Destroy(gameObject, 1f);
    }
}
