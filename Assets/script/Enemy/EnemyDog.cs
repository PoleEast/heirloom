﻿using UnityEngine;
using System.Collections;

public class EnemyDog : Enemy
{
    private GameControl GameControl;
    public float MoveSpeed;
    public float JumpSpeed;
    private BoxCollider2D myFeet;
    private Bounds GroundLocation;
    private bool movedirection;
    protected override void Start()
    {
        base.Start();
        GameControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        myRigidbody = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 2) == 0) movedirection = true;
        else movedirection = false;
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void move(Collider2D Player)
    {
        if (CurrentHP > 0)
        {
            if (checkGound() || !IsAttackRange())
            {
                Vector2 EnemyVel = new Vector2();
                if (Player != null)
                {
                    if (Player.transform.position.x < transform.position.x)
                    {
                        Flip(false);
                        EnemyVel.Set(MoveSpeed * -1, JumpSpeed);
                    }
                    if (Player.transform.position.x > transform.position.x)
                    {
                        Flip(true);
                        EnemyVel.Set(MoveSpeed, JumpSpeed);
                    }
                }
                else if (Player == null)
                {
                    Flip(movedirection);
                    if (gameObject.transform.position.x - GroundLocation.max.x <= -2.5 && gameObject.transform.position.x - GroundLocation.min.x >= 2.5)
                    {
                        if (movedirection == true) EnemyVel.Set(MoveSpeed * 1, JumpSpeed);
                        else EnemyVel.Set(MoveSpeed * -1, JumpSpeed);
                    }
                    else if (gameObject.transform.position.x - GroundLocation.min.x < 2.5)
                    {
                        movedirection = true;
                        Flip(movedirection);
                        EnemyVel.Set(MoveSpeed * 1, JumpSpeed);
                    }
                    else if (gameObject.transform.position.x - GroundLocation.max.x > -2.5)
                    {
                        movedirection = false;
                        Flip(movedirection);
                        EnemyVel.Set(MoveSpeed * -1, JumpSpeed);
                    }
                    else Debug.Log("move QQ");
                }
                myRigidbody.velocity = EnemyVel;
                bool EnemyMove = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
                myAnim.SetBool("move", EnemyMove);
            }
            else if (IsAttackRange())
                attack(IsAttackRange());
        }
    }
    protected override void attack(Collider2D Player)
    {
        myAnim.SetBool("attack", true);
        myAnim.SetBool("idle", false);

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            GroundLocation = other.collider.bounds;
        }
    }
}