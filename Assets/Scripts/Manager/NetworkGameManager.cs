using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameManager : NetworkBehaviour {

    public GameObject Tower;
    public GameObject Nexus;


    private Transform RedTowerSpawn;
    public Transform BlueTowerSpawn;
    public Transform RedNexusSpawn;
    public Transform BlueNexusSpawn;


	// Use this for initialization
	void Awake () {
        RedTowerSpawn = GameObject.FindGameObjectWithTag("RedTowerSpawnPoint").transform;
        GameObject redTower = Instantiate(Tower, RedTowerSpawn);

        Debug.Log(redTower.transform.position);
        NetworkServer.Spawn(redTower);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
