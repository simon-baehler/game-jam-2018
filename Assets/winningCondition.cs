using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winningCondition : MonoBehaviour {


    public GameObject nexusTeam1;
    public GameObject nexusTeam2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (nexusTeam1.GetComponent<HealtManagerNexus>().getCurrentHP() <= 0)
        {
            Debug.Log("Team 2 win");
        }
        if (nexusTeam2.GetComponent<HealtManagerNexus>().getCurrentHP() <= 0){
            Debug.Log("Team 1 win");
        }
	}
}
