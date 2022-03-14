﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttack : MonoBehaviour
{
    private Animator myAnim;
    private Rigidbody2D myRigidbody;
    public float attackmove;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        attack();
    }
    void attack()
    {
        if (Input.GetButtonDown("attack") && !(myAnim.GetBool("attack")))
        {
            if (myAnim.GetBool("idle") || myAnim.GetBool("run"))
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<AttackHitBox>().attack();
                myRigidbody.velocity = new Vector2(0.0f, 0.0f);
                myAnim.SetBool("attack", true);
                myAnim.SetBool("idle", false);
                StartCoroutine(IAttackWork());
            }
            //else if
        }
    }
    protected IEnumerator IAttackWork()
    {
        yield return new WaitForSeconds(0.2f);
        if (transform.localRotation.y == 0)
            myRigidbody.velocity = new Vector2(attackmove, 0.0f);
        else
            myRigidbody.velocity = new Vector2(attackmove * -1, 0.0f);
        myAnim.SetBool("idle", true);
        myAnim.SetBool("attack", false);
    }
}
