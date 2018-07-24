﻿using UnityEngine;

public class Player : MonoBehaviour {

    // Config
    [SerializeField] float damage = 5f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 0f);

    // State
    bool isAlive = true;
    bool isBlocking = false;
    float life = 100f;

    // Cached component references
    CapsuleCollider2D m_BodyCollider;
    Rigidbody2D myRigidBody;
    Collider2D myCollider;
    Animator myAnimator;
    float gravityScaleAtStart;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        m_BodyCollider = GetComponent<CapsuleCollider2D>();

        gravityScaleAtStart = myRigidBody.gravityScale;
    }
	
	// Update is called once per frame
	void Update () {
        if(!isAlive)
        {
            return;
        }

        Attack();
        Block();
        Run();
        Jump();
        FlipSprite();
    }
    
    public void TakeDamage(float damage)
    {
        life -= damage;

        if (life <= 0)
        {
            life = 0;
            isAlive = false;
        }
    }

    private void Attack()
    {

    }

    private void Block()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            isBlocking = true;
            myAnimator.SetBool("Blocking", true);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isBlocking = false;
            myAnimator.SetBool("Blocking", false);
            myAnimator.SetBool("Idle", true);
        }
        
        
    }

    private void Run()
    {
        if (isBlocking)
        {
            return;
        }

        float controlThrow = Input.GetAxis("Horizontal"); // between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", hasHorizontalSpeed);
    }

    private void Jump()
    {
        int layerMask = LayerMask.GetMask("Ground");
        bool isTouchingGround = myCollider.IsTouchingLayers(layerMask);

        if (isTouchingGround && Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity = jumpVelocity;
        }
    }

    private void FlipSprite()
    {
        float XVelocity = myRigidBody.velocity.x;
        bool hasHorizontalSpeed = Mathf.Abs(XVelocity) > Mathf.Epsilon;

        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(XVelocity), 1);
        }
    }
}
