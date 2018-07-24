using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D m_RigidBody;

	// Use this for initialization
	void Start () {
        m_RigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(IsFacingRight())
        {
            m_RigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            m_RigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
        
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(m_RigidBody.velocity.x)), 1f);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}
