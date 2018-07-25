using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    protected int currentTarget;
    protected GameObject targetGameObject;
    protected minion minion;

    protected bool haveAFocus = false;
    protected float timeLeftBeforeShooting = 5.0f;
    protected bool canShoot = true;


    int Team1TowerIndex;
    int Team1bulletIndex;
    int Team2TowerIndex;
    int Team2bulletIndex;

    const float SHOOTING_INTERVAL = 5.0f;
    // Use this for initialization
    void Start()
    {
        haveAFocus = false;
        canShoot = true;
        timeLeftBeforeShooting = SHOOTING_INTERVAL;
    }
    void Update()
    {
        if (!canShoot)
        {
            timeLeftBeforeShooting -= Time.deltaTime;
            if (timeLeftBeforeShooting < 0)
            {
                this.canShoot = true;
            }

        }
    }

    void Fire(Vector2 targetPosition)
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        // Add velocity to the bullet
        int Team1TowerIndex = LayerMask.NameToLayer("Team1-Tower");
        int Team1bulletIndex = LayerMask.NameToLayer("Team1-Bullet");
        int Team2TowerIndex = LayerMask.NameToLayer("Team2-Tower");
        int Team2bulletIndex = LayerMask.NameToLayer("Team2-Bullet");

        if (this.gameObject.layer == Team1TowerIndex) {
            bullet.layer = Team1bulletIndex;
        }else{
            bullet.layer = Team2bulletIndex;
        }
        Vector2 targetVect = new Vector2(targetPosition.x - this.transform.position.x  , targetPosition.y - this.transform.position.y);
        bullet.GetComponent<Rigidbody2D>().velocity = targetVect;
        Destroy(bullet, 5.0f);
    }


    void OnTriggerEnter2D(Collider2D col)
    {

        if (!haveAFocus) {
            //TODO : remplacer name par tag
            if (col.gameObject.tag == "minion")
            {
                Vector2 targetPosition = col.gameObject.transform.position;
                currentTarget = col.gameObject.GetInstanceID();
                haveAFocus = true;
                targetGameObject = col.gameObject;
                if (canShoot)
                {
                    canShoot = false;
                    timeLeftBeforeShooting = 5.0f;   
                    this.Fire(targetPosition);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!haveAFocus)
        {
            //TODO : remplacer name par tag
            if (col.gameObject.tag == "minion")
            {
                Vector2 targetPosition = col.gameObject.transform.position;
                currentTarget = col.gameObject.GetInstanceID();
                targetGameObject = col.gameObject;
                haveAFocus = true;
                if (canShoot)
                {
                    canShoot = false;
                    timeLeftBeforeShooting = 5.0f;
                    this.Fire(targetPosition);
                }
            }
        }
        else {
            Vector2 targetPosition = targetGameObject.transform.position;
            currentTarget = col.gameObject.GetInstanceID();
            haveAFocus = true;
            if (canShoot)
            {
                canShoot = false;
                timeLeftBeforeShooting = 5.0f;
                this.Fire(targetPosition);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //set target null
        if (targetGameObject.GetInstanceID() == collision.GetInstanceID()) {
            currentTarget = 0;
            targetGameObject = null;
            haveAFocus = false;
        }
       
    }
}
