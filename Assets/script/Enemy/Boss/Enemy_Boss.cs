using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy
{
    private GameControl GameControl;
    public float MoveSpeed;
    public float AttackCDSpeed;
    public float SkillCDSpeed;
    public float AttackMoveSpeed;
    public GameObject SkillFlash;
    private Bounds GroundLocation;
    private bool movedirection;
    private bool attackCD;
    private bool Skill1CD;
    private bool attackState;

    protected override void Start()
    {
        base.Start();
        GameControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        myRigidbody = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 2) == 0) movedirection = true;
        else movedirection = false;
        attackState = false;
        StartCoroutine(ISkill1CD());
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
            if (!attackState)
            {
                if (Skill1CD)
                    skill1();
                else if (IsAttackRange())
                {
                    attack(IsAttackRange());
                }
                else if (checkGound())
                    move(IsPlayerview());
            }
        }
        else
            Die();
    }
    protected override void attack(Collider2D Player)
    {
        attackState = true;
        myAnim.SetBool("move", false);
        myAnim.SetBool("idle", false);
        myAnim.SetBool("attack", true);
    }
    void attackMoveWork()
    {
        Vector2 EnemyVel = new Vector2();
        if (movedirection)
            EnemyVel.Set(AttackMoveSpeed * 1, 3);
        else
            EnemyVel.Set(AttackMoveSpeed * -1, 3);
        myRigidbody.velocity = EnemyVel;
    }
    void skill1()
    {
        attackState = true;
        Vector2 EnemyVel = new Vector2();
        Invoke("InstantiateFlash", 1);
        EnemyVel.Set(0, 0);
        myRigidbody.velocity = EnemyVel;
        myAnim.SetBool("move", false);
        myAnim.SetBool("idle", false);
        myAnim.SetBool("skill1", true);
        Skill1CD = false;
        StartCoroutine(ISkill1CD());
    }
    void InstantiateFlash()
    {
        Vector3 vector3 = new Vector3();
        vector3 = gameObject.transform.position;
        vector3.x -= 20;
        for (int i = 0; i < 4; i++)
        {
            vector3.x += 8;
            Instantiate(SkillFlash, vector3, Quaternion.identity);
        }
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
        attackState = false;
    }
    void animationSkill1off()
    {
        myAnim.SetBool("idle", true);
        myAnim.SetBool("skill1", false);
        attackState = false;
    }
    IEnumerator ItakeDamageshark()
    {
        myRigidbody.velocity = new Vector2(-0.5f, 0.5f);
        yield return null;
    }
    IEnumerator ISkill1CD()
    {
        yield return new WaitForSeconds(SkillCDSpeed);
        Skill1CD = true;
    }
}
