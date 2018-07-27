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
        MinionHealthManager minion = collision.GetComponent<MinionHealthManager>();
        PlayerHealthManager player = collision.GetComponent<PlayerHealthManager>();


        
        int idTeam2TowerLayer;
        int idTeam2Nexus;
        int idTeam2Minion;
        int idTeamPlayer;

        if (this.gameObject.layer == 15)
        {
            idTeam2TowerLayer = LayerMask.NameToLayer("Team2-Tower");
            idTeam2Nexus = LayerMask.NameToLayer("Team2-Tower");
            idTeam2Minion = LayerMask.NameToLayer("Team2-Minion");
            idTeamPlayer = LayerMask.NameToLayer("Team2-Player");
        }
        else {
            idTeam2TowerLayer = LayerMask.NameToLayer("Team1-Tower");
            idTeam2Nexus = LayerMask.NameToLayer("Team1-Tower");
            idTeam2Minion = LayerMask.NameToLayer("Team1-Minion");
            idTeamPlayer = LayerMask.NameToLayer("Team1-Player");
        }

        if (collision.gameObject.layer == idTeam2TowerLayer && collision.tag != "Nexus")
        {
            if (!collision.isTrigger) {
                Destroy(gameObject);
                towerH.TakeDamage(1);
            }
        }
        else if (collision.gameObject.layer == idTeam2Nexus && collision.tag == "Nexus")
        {
            if (!collision.isTrigger)
            {
                Destroy(gameObject);
                nexus.TakeDamage(1);
            }
        }
        else if (collision.gameObject.layer == idTeam2Minion && collision.tag == "minion")
        {
            if (!collision.isTrigger)
            {
                Destroy(gameObject);
                minion.TakeDamage(2);
            }
        }
        else if (collision.gameObject.layer == idTeamPlayer && collision.tag == "Player")
        {
            if (!collision.isTrigger)
            {
                Destroy(gameObject);
                player.TakeDamage(2);
            }
        }

    }
}
