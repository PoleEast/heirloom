using UnityEngine;
using System.Collections;

public class EnemySlime : Enemy
{
    private GameControl GameControl;
    public float MoveSpeed;
    public float JumpSpeed;
    public float MoveCDSpeed;
    public float AttackCDSpeed;
    private Bounds GroundLocation;
    private bool movedirection;
    private bool moveCD;
    private bool attackCD;
    protected override void Start()
    {
        base.Start();
        GameControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        myRigidbody = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 2) == 0) movedirection = true;
        else movedirection = false;
        moveCD = true;
    }
    protected override void Update()
    {
        base.Update();
        StateCheese();
    }
    void StateCheese()
    {
        if (HP >= 0)
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
        myAnim.SetBool("attack", true);
        myAnim.SetBool("idle", false);
    }
    protected override void move(Collider2D Player)
    {
        if (moveCD)
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
                else Debug.Log(this + "NO move");
            }
            myRigidbody.velocity = EnemyVel;
            moveCD = false;
            myAnim.SetBool("move", true);
            myAnim.SetBool("idle", false);
            StartCoroutine(IMoveCD());
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            GroundLocation = other.collider.bounds;
        }
    }
    void animationMoveOff()
    {
        myAnim.SetBool("idle", true);
        myAnim.SetBool("move", false);

    }
    IEnumerator ItakeDamageshark()
    {
        myRigidbody.velocity = new Vector2(-0.5f, 0.5f);
        yield return null;
    }
    IEnumerator IMoveCD()
    {
        yield return new WaitForSeconds(MoveCDSpeed);
        moveCD = true;
    }
}
