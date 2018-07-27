using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManageMinion : NetworkBehaviour {

    public GameObject Enemy;
    GameObject EnemyClone;
    public GameObject pointDepart;
    public float spawnFrequency = 5f;

    private float timer = 5f;

	// Update is called once per frame
	void Update () {
        if (!isServer)
        {
            return;
        }
        //Insertion d'un Minion chaque 5 secondes à la position "pointDepart"
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            EnemyClone = Instantiate(Enemy, pointDepart.transform.position, Quaternion.identity);
            NetworkServer.Spawn(EnemyClone);

            timer = spawnFrequency;
        }
    }
}

