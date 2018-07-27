using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHealthManager : MonoBehaviour {

    public const float maxHealth = 100f;
    public float currentHealth = maxHealth;
    public RectTransform healthBar;


    public void Start()
    {
        if(this.gameObject.layer == 17)
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
    private void Update()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        if (currentHealth == 0)
        {
            Death();
        }
    }

    public void TakeDamage(float amount)
    {
        StartCoroutine(wait());
         
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
        
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    IEnumerator wait()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        if (this.gameObject.layer == 17) {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }else{
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
           
        
    }   

    public void Death()
    {
        Destroy(gameObject);
    }
}
