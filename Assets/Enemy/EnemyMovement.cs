using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D m_RigidBody;
    Collider2D m_Collider;
    public LayerMask layerMask;
    private string status = "moving";
    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    // Use this for initialization
    void Start () {
        m_RigidBody = GetComponent<Rigidbody2D> ();
        m_Collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (status == "moving")
        {
            if (IsFacingRight())
            {
                m_RigidBody.velocity = new Vector2(moveSpeed, 0f);
            }
            else
            {
                m_RigidBody.velocity = new Vector2(-moveSpeed, 0f);
            }
        }

        if (DetectedTower()) {
            Debug.Log("Detected2");
            m_RigidBody.velocity = new Vector2(0f, 0f);
            Fire();
        };



    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(m_RigidBody.velocity.x)), 1f);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int idTeam1_tower = LayerMask.NameToLayer("Team1-Tower");
        int idTeam2_tower = LayerMask.NameToLayer("Team2-Tower");

        int m_layer = this.gameObject.layer;

        if (collision.gameObject.layer == idTeam1_tower || collision.gameObject.layer == idTeam2_tower) {
            //stop moving and attack
            m_RigidBody.velocity = new Vector2(0f, 0f);
            status = "attacking";
        }

    }*/

    bool DetectedTower()
    {
        return Physics2D.Raycast(transform.position, transform.right, 10 , layerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + 1000,transform.position.y,0));
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            this.gameObject.transform.position,
            this.gameObject.transform.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    public void TakeDamage(float Damage)
    {
        Destroy(gameObject);
    }
}
