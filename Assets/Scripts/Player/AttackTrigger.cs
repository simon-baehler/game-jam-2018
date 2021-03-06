﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AttackTrigger : NetworkBehaviour {

    public float damage = 10f;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.isTrigger != true && (coll.CompareTag("minion") || coll.CompareTag("Tower")))
        {
            coll.gameObject.BroadcastMessage("TakeDamage", damage);
        }
        Debug.Log("OnTriggerEnter2D");
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.isTrigger != true && (coll.CompareTag("minion") || coll.CompareTag("Tower")))
        {
            coll.gameObject.BroadcastMessage("TakeDamage", damage);
        }
        Debug.Log("OnTriggerStay2D");
    }
}
