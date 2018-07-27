using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManageMinion : NetworkBehaviour {

    public GameObject Enemy;
    public GameObject Enemy2;
    public GameObject Enemy3;
    GameObject EnemyClone;
    public GameObject pointDepart;
<<<<<<< HEAD
    public float spawnFrequency = 5f;

    private float timer = 5f;
=======
    private float timeToInsert = 15f;
    public int team;
>>>>>>> master

	// Update is called once per frame
	void Update () {
        if (!isServer)
        {
            return;
        }
        //Insertion d'un Minion chaque 5 secondes à la position "pointDepart"
<<<<<<< HEAD
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            EnemyClone = Instantiate(Enemy, pointDepart.transform.position, Quaternion.identity);
            NetworkServer.Spawn(EnemyClone);

            timer = spawnFrequency;
=======
        timeToInsert -= Time.deltaTime;
       
        if (timeToInsert <= 0f)
        {
            Instantiate(Enemy, pointDepart.transform.position, Quaternion.identity);
            Instantiate(Enemy2, pointDepart.transform.position + new Vector3(1,0,0), Quaternion.identity);
            Instantiate(Enemy3, pointDepart.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
            timeToInsert = 15f;
>>>>>>> master
        }
    }

    public int getTeam() {
        return team;
    }
}

