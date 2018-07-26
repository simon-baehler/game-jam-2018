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

        int idTeam2TowerLayer = LayerMask.NameToLayer("Team2-Tower");

        if (collision.gameObject.layer == idTeam2TowerLayer)
        { 
            Destroy(gameObject);
            towerH.TakeDamage(5);
        }
       Debug.Log(collision.gameObject.layer);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("FUIFUI");
    }
}
