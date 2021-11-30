using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Runspeed;
    public float Jumpspeed;
    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        run();
        flip();
        jump();
        checkGround();
        switchAnimation();
    }
    private void checkGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    private void flip()
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
    private void run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * Runspeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playerHasXSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("run", playerHasXSpeed);
    }
    private void jump()
    {
        if (Input.GetButtonDown("Jump") && isGround == true)
        {
            myAnim.SetBool("jump", true);
            myAnim.SetBool("idle", false);
            Vector2 jumpVel = new Vector2(0.0f, Jumpspeed);
            myRigidbody.velocity = Vector2.up * jumpVel;
        }
    }
    private void switchAnimation()
    {
        if (myAnim.GetBool("jump"))
        {
            if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && myRigidbody.velocity.y < 0.1f)
            {
                myAnim.SetBool("jump", false);
                myAnim.SetBool("idle", true);
            }
        }
    }
    // private void attack()
    // {
    //     if (Input.GetButtonDown("attack") && myAnim.GetCurrentAnimatorStateInfo(0).IsName("sword") == false)
    //     {
    //         myAnim.SetTrigger("attack");
    //     }
    // }
}

