using Cinemachine;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    // Config
    [SerializeField] float damage = 5f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 0f);

    // State
    float life = 100f;
    bool isAlive = true;
    bool isBlocking = false;

    // Cached component references
    CinemachineVirtualCamera myCamera;
    SpriteRenderer mySpriteRenderer;
    CapsuleCollider2D m_BodyCollider;
    Rigidbody2D myRigidBody;
    Collider2D myCollider;
    Animator myAnimator;
    float gravityScaleAtStart;

    // Spawn points
    private NetworkStartPosition[] spawnPoints;
    public GameObject respawnPostion;

	// Use this for initialization
	void Awake () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        m_BodyCollider = GetComponent<CapsuleCollider2D>();

        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            Vector3 spawnPoint = Vector3.zero;

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;

            /*RpcRespawn();*/
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(!isLocalPlayer)
        {
            return;
        }

        if(!isAlive)
        {
            return;
        }

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
            Death();
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            myAnimator.SetTrigger("IsAttacking");
        }

    }

    private void Block()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            isBlocking = true;
            myAnimator.SetBool("Idle", false);
            myAnimator.SetBool("Running", false);
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
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        float diff = Mathf.Abs(mousePosition.x) - Mathf.Abs(transform.position.x);
        transform.localScale = new Vector2(Mathf.Sign(diff), 1);
    }

    public void Death()
    {
        //Death();
        this.transform.position = respawnPostion.transform.position;
        this.gameObject.GetComponent<PlayerHealthManager>().revive();
    }

    // --------------- Network -------------------
    public override void OnStartLocalPlayer()
    {
        PolygonCollider2D background = GameObject.FindWithTag("Background").GetComponent<PolygonCollider2D>();
        // base.OnStartLocalPlayer();
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        mySpriteRenderer.color = new Color(0.5f, 0.7f, 1f, 1f);

        myCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        CinemachineConfiner cameraConfiner = GetComponentInChildren<CinemachineConfiner>();

        myCamera.enabled = true;
        myCamera.Follow = transform;
        cameraConfiner.m_BoundingShape2D = background;
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;

            if(spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;
        }
    }
}
