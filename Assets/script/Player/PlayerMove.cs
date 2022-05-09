using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private BoxCollider2D myFeet;
    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    public float Runspeed;
    public float Jumpspeed;
    public float Rollspeed;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

    }
    void LateUpdate()
    {
        run();
        jump();
        roll();
        doubleJump();
    }

    void run()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("sword") == false &&
                (myAnim.GetCurrentAnimatorStateInfo(0).IsName("roll") == false) &&
                myAnim.GetBool("roll") == false)
            {
                float moveDir = Input.GetAxis("Horizontal");
                moveforWard(moveDir * Runspeed, myRigidbody.velocity.y);
                bool playerHasXSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
                myAnim.SetBool("run", playerHasXSpeed);
            }
        }
        else
            myAnim.SetBool("run", false);
    }
    void jump()
    {
        if (Input.GetButtonDown("Jump") && GetComponent<PlayerStats>().checkGround())
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
                moveforWard(Rollspeed, myRigidbody.velocity.y);
            else
                moveforWard(Rollspeed * -1, myRigidbody.velocity.y);
            StartCoroutine(IrollEnd());
        }
    }
    void moveforWard(float Xspeed, float Yspeed)
    {
        Vector2 playerVel = new Vector2(Xspeed, Yspeed);
        myRigidbody.velocity = playerVel;
    }
    void doubleJump()
    {

    }
    IEnumerator IrollEnd()
    {
        yield return new WaitForSeconds(0.6f);
        myAnim.SetBool("idle", true);
        myAnim.SetBool("roll", false);
    }
}
