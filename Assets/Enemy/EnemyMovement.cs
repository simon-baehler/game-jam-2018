﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D m_RigidBody;
    Collider2D m_Collider;
    public LayerMask layerMask;
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

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(m_RigidBody.velocity.x)), 1f);
    }*/

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    bool DetectedTower()
    {
        return Physics2D.Raycast(transform.position, transform.right, 5 , layerMask);
    }

   /* private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + 1000,transform.position.y,0));
    }*/

    void Fire()
    {
        timeFire -= Time.deltaTime;
        if (timeFire <= 0)
        {           
            Instantiate(bulletPrefab, firePos.transform.position, Quaternion.identity);
            timeFire = 1f;
        }
    }
}
