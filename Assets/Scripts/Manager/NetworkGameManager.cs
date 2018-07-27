using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameManager : NetworkBehaviour {

    const string BLUE_TOWER_LAYER = "Team1-Tower";
    const string RED_TOWER_LAYER = "Team2-Tower";

    public GameObject Tower;
    public GameObject Nexus;

    public Sprite BlueNexus;
    public Sprite RedNexus;

    private Transform RedTowerSpawn;
    private Transform BlueTowerSpawn;
    private Transform RedNexusSpawn;
    private Transform BlueNexusSpawn;


	// Use this for initialization
	void Awake () {

        // ------------- Red Team -------------
        RedTowerSpawn = GameObject.FindGameObjectWithTag("RedTowerSpawnPoint").transform;
        GameObject redTower = Instantiate(Tower, RedTowerSpawn);
        redTower.layer = LayerMask.NameToLayer(RED_TOWER_LAYER);

        RedNexusSpawn = GameObject.FindGameObjectWithTag("RedNexusSpawnPoint").transform;
        GameObject redNexus = Instantiate(Nexus, RedNexusSpawn);
        redNexus.layer = LayerMask.NameToLayer(RED_TOWER_LAYER);

        // ------------- Blue Team -------------
        BlueTowerSpawn = GameObject.FindGameObjectWithTag("BlueTowerSpawnPoint").transform;
        GameObject blueTower = Instantiate(Tower, BlueTowerSpawn);
        blueTower.layer = LayerMask.NameToLayer(BLUE_TOWER_LAYER);

        BlueNexusSpawn = GameObject.FindGameObjectWithTag("BlueNexusSpawnPoint").transform;
        GameObject blueNexus = Instantiate(Nexus, BlueNexusSpawn);
        blueNexus.layer = LayerMask.NameToLayer(BLUE_TOWER_LAYER);

        // Spawn EVERYTHING
        NetworkServer.Spawn(redTower);
        NetworkServer.Spawn(blueTower);
        NetworkServer.Spawn(redNexus);
        NetworkServer.Spawn(blueNexus);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
