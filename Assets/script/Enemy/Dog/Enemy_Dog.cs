using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dog : Enemy
{
    private GameControl GameControl;
    public float MoveSpeed;
    public float AttackCDSpeed;
    public float AttackMoveSpeed;
    private bool movedirection;
    private bool attackCD;

    protected override void Start()
    {
        base.Start();
        GameControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        myRigidbody = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 2) == 0) movedirection = true;
        else movedirection = false;
        StartCoroutine(IAttackCD());
    }
    protected override void Update()
    {
        base.Update();
        StateCheese();
    }
    void StateCheese()
    {
        if (CurrentHP > 0)
        {
            if (checkGound() && !attackCD)
            {
                if (IsAttackRange())
                {
                    attack(IsAttackRange());
                }
                else
                    move(IsPlayerview());
            }
        }
        else
            Die();
    }
    protected override void attack(Collider2D Player)
    {
        myAnim.SetBool("move", false);
        myAnim.SetBool("idle", false);
        myAnim.SetBool("attack", true);
        StartCoroutine(IAttackCD());
    }
    void attackMoveWork()
    {
        Vector2 EnemyVel = new Vector2();
        if (movedirection)
            EnemyVel.Set(AttackMoveSpeed * 1, 0);
        else
            EnemyVel.Set(AttackMoveSpeed * -1, 0);
        myRigidbody.velocity = EnemyVel;
    }
    protected override void move(Collider2D Player)
    {

        Vector2 EnemyVel = new Vector2();
        if (Player != null)
        {
            if (Player.transform.position.x < transform.position.x)
            {
                movedirection = false;
                Flip(movedirection);
                if (myAnim.GetBool("move") || myAnim.GetBool("idle"))
                {
                    EnemyVel.Set(MoveSpeed * -1, 0);
                }
            }
            if (Player.transform.position.x > transform.position.x)
            {
                movedirection = true;
                Flip(movedirection);
                if (myAnim.GetBool("move") || myAnim.GetBool("idle"))
                {
                    EnemyVel.Set(MoveSpeed, 0);
                }
            }
        }
        else if (Player == null)
        {
            Flip(movedirection);
            if (gameObject.transform.position.x - GroundLocation.max.x < 0 && gameObject.transform.position.x - GroundLocation.min.x > 0)
            {
                if (movedirection == true) EnemyVel.Set(MoveSpeed * 1, 0);
                else EnemyVel.Set(MoveSpeed * -1, 0);
            }
            else if (gameObject.transform.position.x - GroundLocation.min.x <= 0)
            {
                movedirection = true;
                Flip(movedirection);
                EnemyVel.Set(MoveSpeed * 1, 0);
            }
            else if (gameObject.transform.position.x - GroundLocation.max.x >= 0)
            {
                movedirection = false;
                Flip(movedirection);
                EnemyVel.Set(MoveSpeed * -1, 0);
            }
            else Debug.Log(this + "NO move");
        }
        myRigidbody.velocity = EnemyVel;
        myAnim.SetBool("move", true);
        myAnim.SetBool("idle", false);

    }
    void animationAttackoff()
    {
        myAnim.SetBool("idle", true);
        myAnim.SetBool("attack", false);
    }
    IEnumerator ItakeDamageshark()
    {
        myRigidbody.velocity = new Vector2(-0.5f, 0.5f);
        yield return null;
    }
    IEnumerator IAttackCD()
    {
        attackCD = true;
        yield return new WaitForSeconds(AttackCDSpeed);
        attackCD = false;
    }
}
