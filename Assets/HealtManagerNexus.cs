using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealtManagerNexus : NetworkBehaviour {

    public const float maxHealth = 400f;
    
    public RectTransform healthBar;

    [SyncVar]
    public float currentHealth = maxHealth;

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

        Debug.Log("Nexus took damage");
        
        currentHealth -= amount;
        if (currentHealth <= 0)
        {       
            currentHealth = 0;
            Death();
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    public float getCurrentHP()
    {
        return currentHealth;
    }

    public void Death()
    {
        TowerAudio.Play();
        Destroy(gameObject, 1f);
    }
}
