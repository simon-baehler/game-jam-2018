using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float damage = 10f;

    void Start ()
    {
        Debug.Log("Fired");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "minion") {
            Debug.Log("Hit");
            EnemyMovement enemyScript = collision.gameObject.GetComponent<EnemyMovement>();
            enemyScript.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
