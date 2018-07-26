using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMinion : MonoBehaviour {

    public GameObject Enemy;
    GameObject EnemyClone;
    public GameObject pointDepart;
    private float timeToInsert = 3f;

	// Update is called once per frame
	void Update () {
        //Insertion d'un Minion chaque 5 secondes à la position "pointDepart"
        timeToInsert -= Time.deltaTime;
        if(timeToInsert <= 0)
        {
            EnemyClone = Instantiate(Enemy, pointDepart.transform.position, Quaternion.identity) as GameObject;
            timeToInsert = 3f;
        }
    }
}

