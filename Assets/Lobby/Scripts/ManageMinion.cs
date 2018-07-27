using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManageMinion : NetworkBehaviour {

    public GameObject Enemy;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject pointDepart;

    public float spawnFrequency = 5f;
    private float timer = 5f;
    public int team;

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
            GameObject minion1 = Instantiate(Enemy, pointDepart.transform.position, Quaternion.identity);
            GameObject minion2 = Instantiate(Enemy2, pointDepart.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            GameObject minion3 = Instantiate(Enemy3, pointDepart.transform.position + new Vector3(2, 0, 0), Quaternion.identity);

            NetworkServer.Spawn(minion1);
            NetworkServer.Spawn(minion2);
            NetworkServer.Spawn(minion3);

            timer = spawnFrequency;
        }
    }

    public int getTeam() {
        return team;
    }
}

