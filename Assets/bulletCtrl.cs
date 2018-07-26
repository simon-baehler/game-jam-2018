using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCtrl : MonoBehaviour {

    public Vector2 speed;
    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;
    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthManager towerH = collision.GetComponent<HealthManager>();
        HealtManagerNexus nexus = collision.GetComponent<HealtManagerNexus>();

        int idTeam2TowerLayer = LayerMask.NameToLayer("Team2-Tower");
        int idTeam2Nexus = LayerMask.NameToLayer("Team2-Nexus");

        if (collision.gameObject.layer == idTeam2TowerLayer)
        { 
            Destroy(gameObject);
            towerH.TakeDamage(5);
        }
        if(collision.gameObject.layer == idTeam2Nexus)
        {
            Destroy(gameObject);
            nexus.TakeDamage(5);
        }
       Debug.Log(collision.gameObject.layer);
    }
}
