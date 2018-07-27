using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMinion : MonoBehaviour {

    public GameObject Enemy;
    public GameObject Enemy2;
    public GameObject Enemy3;
    GameObject EnemyClone;
    public GameObject pointDepart;
    private float timeToInsert = 15f;
    public int team;

	// Update is called once per frame
	void Update () {
        //Insertion d'un Minion chaque 5 secondes à la position "pointDepart"
        timeToInsert -= Time.deltaTime;
       
        if (timeToInsert <= 0f)
        {
            Instantiate(Enemy, pointDepart.transform.position, Quaternion.identity);
            Instantiate(Enemy2, pointDepart.transform.position + new Vector3(1,0,0), Quaternion.identity);
            Instantiate(Enemy3, pointDepart.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
            timeToInsert = 15f;
        }
    }

    public int getTeam() {
        return team;
    }
}

