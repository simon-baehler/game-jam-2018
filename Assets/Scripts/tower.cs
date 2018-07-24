using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    public bool haveAFocus = false;
    public int currentTarget;
    public GameObject targetGameObject;
    public minion minion;

    public float timeLeft = 5.0f;
    public bool canShoot = true;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;



    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {

        if (!canShoot)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                this.canShoot = true;
            }

        }
    }


    private void OnDrawGizmos()
    {
        if (targetGameObject) {
            Gizmos.color = Color.red;
            Vector3 position = targetGameObject.transform.position;
            Gizmos.DrawSphere(position, 0.1f);
        }

    }

    void Fire(Vector2 targetPosition)
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        int direction = 6;
        // Add velocity to the bullet

        if (this.gameObject.layer == 11) {
            bullet.layer = 10;
        }else{
            bullet.layer = 9;
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
                    timeLeft = 5.0f;   
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
                    timeLeft = 5.0f;
                    this.Fire(targetPosition);
                }
            }
        }
        else {
            Debug.Log("have focus");
            Vector2 targetPosition = targetGameObject.transform.position;
            currentTarget = col.gameObject.GetInstanceID();
            haveAFocus = true;
            if (canShoot)
            {
                canShoot = false;
                timeLeft = 5.0f;
                this.Fire(targetPosition);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //set target null
        currentTarget = 0;
        targetGameObject = null;
        haveAFocus = false;
    }
}
