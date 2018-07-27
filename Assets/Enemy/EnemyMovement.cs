using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyMovement : NetworkBehaviour {

    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D m_RigidBody;
    Collider2D m_Collider;
    public LayerMask layerMaskTower;
    public LayerMask layerMaskNexus;
    public LayerMask layerMaskMinion;
    public LayerMask layerMaskPlayer;
    private string status = "moving";
    public GameObject bulletPrefab;
    public Transform firePos;
    private float timeFire = 1f;

    private Renderer rend;


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
            if (this.gameObject.layer == 13)
            {
                m_RigidBody.velocity = new Vector2(moveSpeed, 0f);
            }
            else
            {
                m_RigidBody.velocity = new Vector2(-moveSpeed, 0f);
            }
        }

        if (DetectedTower()) {
            m_RigidBody.velocity = new Vector2(0f, 0f);
            CmdFire();
        };
        if (DetectedNexus()) {
            m_RigidBody.velocity = new Vector2(0f, 0f);
            CmdFire();
        };

        if (DetectedMinion())
        {
            m_RigidBody.velocity = new Vector2(0f, 0f);
            CmdFire();
        };

        if (DetectedPlayer())
        {
            m_RigidBody.velocity = new Vector2(0f, 0f);
            CmdFire();
        };

    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    bool DetectedTower()
    {
        if (this.gameObject.layer == 13)
        {
            return Physics2D.Raycast(transform.position, transform.right, 5, layerMaskTower);
        }
        else {
            return Physics2D.Raycast(transform.position, transform.right, -5, layerMaskTower);
        }
    }
    bool DetectedNexus()
    {
        if (this.gameObject.layer == 13)
        {
            return Physics2D.Raycast(transform.position, transform.right, 5, layerMaskTower);
        }
        else
        {
            return Physics2D.Raycast(transform.position, transform.right, -5, layerMaskTower);
        }
    }


    bool DetectedMinion()
    {
        if (this.gameObject.layer == 13)
        {
            return Physics2D.Raycast(transform.position, transform.right, 5, layerMaskMinion);
        }
        else
        {
            return Physics2D.Raycast(transform.position, transform.right, -5, layerMaskMinion);
        }
    }


    bool DetectedPlayer()
    {
        if (this.gameObject.layer == 13)
        {
            return Physics2D.Raycast(transform.position, transform.right, 5, layerMaskPlayer);
        }
        else
        {
            return Physics2D.Raycast(transform.position, transform.right, -5, layerMaskPlayer);
        }
    }

    [Command]
    void CmdFire()
    {
        timeFire -= Time.deltaTime;
        if (timeFire <= 0)
        {           
            GameObject bullet = Instantiate(bulletPrefab, firePos.transform.position, Quaternion.identity);
            timeFire = 1f;
            NetworkServer.Spawn(bullet);

        }
    }
}
