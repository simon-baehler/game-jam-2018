using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMinion : MonoBehaviour {

    public GameObject Enemy;
    GameObject EnemyClone;
    public GameObject pointDepart;
    private float timeToInsert = 1f;

	// Update is called once per frame
	void Update () {
        //Insertion d'un Minion chaque 5 secondes à la position "pointDepart"
        timeToInsert -= Time.deltaTime;
        if(timeToInsert <= 0)
        {
            Instantiate(Enemy, pointDepart.transform.position, Quaternion.identity);
            timeToInsert = 1f;
        }
    }
}

