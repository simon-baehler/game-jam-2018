using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float damage = 10f;

    void Start ()
    {
        Debug.Log("Fired bullet");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "minion") {
            Debug.Log("Hit");
            MinionHealthManager enemyScript = collision.gameObject.GetComponent<MinionHealthManager>();
            enemyScript.TakeDamage(damage);
        }
        Debug.Log(collision.gameObject.name);
        Destroy(this.gameObject);
    }
}
