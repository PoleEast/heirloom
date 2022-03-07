using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : BaseNumber
{
    public float Runspeed;
    public float Jumpspeed;

    public float attackmove;
    public float Rollspeed;
    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private CapsuleCollider2D mybody;
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        mybody = GetComponent<CapsuleCollider2D>();
        myAnim.SetBool("idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        roll();
        run();
        jump();
        fall();
        fallEnd();
        attack();
    }
    void LateUpdate()
    {
        flip();
    }
    bool checkGround()
    {
        return myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    void flip()
    {
        bool playerHasXSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasXSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
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
    void run()
    {
        if (!(myAnim.GetCurrentAnimatorStateInfo(0).IsName("sword")) && !(myAnim.GetCurrentAnimatorStateInfo(0).IsName("roll")))
        {
            float moveDir = Input.GetAxis("Horizontal");
            moveforWard(moveDir);
            bool playerHasXSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            //if (myAnim.GetBool("jump") == false)
            myAnim.SetBool("run", playerHasXSpeed);
        }
    }
    void moveforWard(float moveDir)
    {
        Vector2 playerVel = new Vector2(moveDir * Runspeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
    }
    void jump()
    {
        if (Input.GetButtonDown("Jump") && checkGround())
        {
            myAnim.SetBool("run", false);
            myAnim.SetBool("jump", true);
            myAnim.SetBool("idle", false);
            Vector2 jumpVel = new Vector2(0.0f, Jumpspeed);
            myRigidbody.velocity = Vector2.up * jumpVel;
        }
    }
    void roll()
    {
        if (Input.GetButtonDown("roll") && (myAnim.GetBool("run") || myAnim.GetBool("idle")) && !(myAnim.GetCurrentAnimatorStateInfo(0).IsName("roll")))
        {
            myAnim.SetBool("roll", true);
            myAnim.SetBool("idle", false);
            myAnim.SetBool("run", false);
            if (transform.localRotation.y == 0)
            {
                myRigidbody.velocity = new Vector2(Rollspeed, 0.0f);
                Debug.Log("");
            }
            else
            {
                myRigidbody.velocity = new Vector2(Rollspeed * -1, 0.0f);
                Debug.Log("");
            }
        }
    }
    void fall()
    {
        doubleJump();
        if (myRigidbody.velocity.y < 0f && myAnim.GetBool("jump") == true)
        {
            myAnim.SetBool("jump", false);
            myAnim.SetBool("fall", true);
        }
    }
    void doubleJump()
    {

    }
    void fallEnd()
    {
        if (myAnim.GetBool("fall") && checkGround())
        {
            myAnim.SetBool("fall", false);
            myAnim.SetBool("idle", true);
        }
    }

    void TakeDamage(int Damage)
    {
        HP = HP - Damage;
    }
    IEnumerator IAttackWork()
    {
        yield return new WaitForSeconds(0.2f);
        if (transform.localRotation.y == 0)
            myRigidbody.velocity = new Vector2(attackmove, 0.0f);
        else
            myRigidbody.velocity = new Vector2(attackmove * -1, 0.0f);
        //StartCoroutine(doubleAttack());
        // if (Input.GetButtonDown("attack") && myAnim.GetCurrentAnimatorStateInfo(0).IsName("sword3") == false)
        // {

        // }
        myAnim.SetBool("idle", true);
        myAnim.SetBool("attack", false);
    }
    void rollEnd()
    {
        myAnim.SetBool("idle", true);
        myAnim.SetBool("roll", false);
    }
    // IEnumerator doubleAttack()
    // {
    //     yield return new WaitForSeconds(0.2f);
    //     Debug.Log
    //     if (Input.GetButtonDown("attack"))
    //     {
    //         myAnim.SetTrigger("attack");
    //     }
    // }
}

