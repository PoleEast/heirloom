using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int HP;
    public int Damage;
    public float flashTime;
    protected Rigidbody2D myRigidbody;
    protected Animator myAnim;
    protected BoxCollider2D myFeet;
    protected CapsuleCollider2D mybody;
    protected bool live;
    protected bool oriented; //true=R false=f
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
        fall();
        fallEnd();
    }
    void LateUpdate()
    {
        flip();
        die();
    }
    public bool checkGround()
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
                oriented = true;
            }
            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                oriented = false;
            }
        }
    }
    void die()
    {
        if (HP == 0)
        {
            myAnim.SetTrigger("die");
            if (oriented)
                moveforWard(-4f, 4f);
            else if (oriented)
                moveforWard(4f, 4f);
            live = false;
        }
    }

    void fall()
    {
        if (myRigidbody.velocity.y < 0f && myAnim.GetBool("jump") == true)
        {
            myAnim.SetBool("jump", false);
            myAnim.SetBool("fall", true);
        }
    }
    void fallEnd()
    {
        if (myAnim.GetBool("fall") && checkGround())
        {
            myAnim.SetBool("fall", false);
            myAnim.SetBool("idle", true);
        }
    }
    public void TakeDamage(int Damage)
    {
        HP = HP - Damage;
    }
    void moveforWard(float Xspeed, float Yspeed)
    {
        Vector2 playerVel = new Vector2(Xspeed, Yspeed);
        myRigidbody.velocity = playerVel;
    }
}

