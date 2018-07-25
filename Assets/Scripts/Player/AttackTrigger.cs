using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public float damage = 10f;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.isTrigger != true && coll.CompareTag("Enemy"))
        {
            coll.SendMessageUpwards("TakeDamage", damage);
        }
    }
}
