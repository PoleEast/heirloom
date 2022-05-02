using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy
{
    private GameControl GameControl;
    public float MoveSpeed;
    public float MoveCDSpeed;
    public float AttackCDSpeed;
    public float SkillCDSpeed;
    public float AttackMoveSpeed;
    private Bounds GroundLocation;
    private bool movedirection;
    private bool moveCD;
    private bool attackCD;
    private bool canMove;

    protected override void Start()
    {
        base.Start();
        GameControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        myRigidbody = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 2) == 0) movedirection = true;
        else movedirection = false;
        moveCD = true;
        canMove = true;
    }
    protected override void Update()
    {
        base.Update();
        StateCheese();
    }
    void StateCheese()
    {
        if (HP > 0)
        {

            if (checkGound())
                if (IsAttackRange())
                {
                    attack(IsAttackRange());
                }
                else
                    move(IsPlayerview());

        }
        else
            Die();
    }
    protected override void attack(Collider2D Player)
    {
        canMove = false;
        myAnim.SetBool("move", false);
        myAnim.SetBool("idle", false);
        myAnim.SetBool("attack", true);
    }
    protected override void move(Collider2D Player)
    {
        if (canMove)
        {
            Vector2 EnemyVel = new Vector2();
            if (Player != null)
            {
                if (Player.transform.position.x < transform.position.x)
                {
                    Flip(false);
                    EnemyVel.Set(MoveSpeed * -1, 0);
                }
                if (Player.transform.position.x > transform.position.x)
                {
                    Flip(true);
                    EnemyVel.Set(MoveSpeed, 0);
                }
            }
            else if (Player == null)
            {
                Flip(movedirection);
                if (gameObject.transform.position.x - GroundLocation.max.x <= -2.5 && gameObject.transform.position.x - GroundLocation.min.x >= 2.5)
                {
                    if (movedirection == true) EnemyVel.Set(MoveSpeed * 1, 0);
                    else EnemyVel.Set(MoveSpeed * -1, 0);
                }
                else if (gameObject.transform.position.x - GroundLocation.min.x < 2.5)
                {
                    movedirection = true;
                    Flip(movedirection);
                    EnemyVel.Set(MoveSpeed * 1, 0);
                }
                else if (gameObject.transform.position.x - GroundLocation.max.x > -2.5)
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
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            GroundLocation = other.collider.bounds;
        }
    }
    void animationAttackoff()
    {
        myAnim.SetBool("idle", true);
        myAnim.SetBool("attack", false);
        canMove = true;
        Debug.Log("Attackoff");
    }
    IEnumerator ItakeDamageshark()
    {
        myRigidbody.velocity = new Vector2(-0.5f, 0.5f);
        yield return null;
    }
    void attackMoveWork()
    {
        Vector2 EnemyVel = new Vector2();
        Debug.Log(movedirection);
        if (movedirection)
            EnemyVel.Set(AttackMoveSpeed * 1, 0);
        else
            EnemyVel.Set(AttackMoveSpeed * -1, 0);
        myRigidbody.velocity = EnemyVel;
    }
}
