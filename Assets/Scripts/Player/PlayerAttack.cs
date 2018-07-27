using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttack : NetworkBehaviour {

    public float attackSpeed = 1f;


    private bool IsAttacking = false;
    private float attackTimer = 0f;

    public Collider2D attackTrigger;

    private Animator anim;
    private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        attackTrigger.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(!isLocalPlayer)
        {
            return;
        }

		if(Input.GetButtonDown("Fire1") && !IsAttacking)
        {
            IsAttacking = true;
            attackTimer = attackSpeed;
            audioSource.Play();

            CmdAttack();
        }

        if(IsAttacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                IsAttacking = false;
                attackTrigger.enabled = false;
            }
        }

        anim.SetBool("IsAttacking", IsAttacking);
    }

    [Command]
    void CmdAttack()
    {
        attackTrigger.enabled = true;
    }
}
