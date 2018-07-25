using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NexusManager : MonoBehaviour {


    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    protected int currentTarget;
    protected GameObject targetGameObject;
    protected minion minion;
    protected float timeLeftBeforeShooting;
    protected bool haveAFocus = false;
   
    protected bool canShoot = true;


    int Team1TowerIndex;
    int Team1bulletIndex;
    int Team2TowerIndex;
    int Team2bulletIndex;

    const float SHOOTING_INTERVAL = 2.0f;
     
    // Use this for initialization
    void Start()
    {
        haveAFocus = false;
        canShoot = true;
        timeLeftBeforeShooting = SHOOTING_INTERVAL;

        Team1TowerIndex = LayerMask.NameToLayer("Team1-Nexus");
        Team1bulletIndex = LayerMask.NameToLayer("Team1-Bullet");
        Team2TowerIndex = LayerMask.NameToLayer("Team2-Nexus");
        Team2bulletIndex = LayerMask.NameToLayer("Team2-Bullet");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * Descr : 
     * 
     */
    void FireAndRest(Collider2D col)
    {
        Vector2 targetPosition = col.gameObject.transform.position;
        currentTarget = col.gameObject.GetInstanceID();
        haveAFocus = true;
        targetGameObject = col.gameObject;
        if (canShoot)
        {
            canShoot = false;
            timeLeftBeforeShooting = SHOOTING_INTERVAL;
            this.Fire(targetPosition);
        }
    }

    void Fire(Vector2 targetPosition)
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);


        //spawned bullet team = tower team
        if (this.gameObject.layer == Team1TowerIndex)
        {
            bullet.layer = Team1bulletIndex;
        }
        else
        {
            bullet.layer = Team2bulletIndex;
        }

        Vector2 targetVect = new Vector2(targetPosition.x - this.transform.position.x, targetPosition.y - this.transform.position.y);
        bullet.GetComponent<Rigidbody2D>().velocity = targetVect;
        Destroy(bullet, SHOOTING_INTERVAL);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!haveAFocus)
        {

            Debug.Log("1");
            if (col.gameObject.tag == "minion")
            {
                FireAndRest(col);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!haveAFocus)
        {
            FireAndRest(col);
        }
        else
        {
            Vector2 targetPosition = targetGameObject.transform.position;
            currentTarget = col.gameObject.GetInstanceID();
            haveAFocus = true;
            if (canShoot)
            {
                canShoot = false;
                timeLeftBeforeShooting = SHOOTING_INTERVAL;
                this.Fire(targetPosition);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //set target null
        if (targetGameObject.GetInstanceID() == collision.GetInstanceID())
        {
            currentTarget = 0;
            targetGameObject = null;
            haveAFocus = false;
        }

    }
}
