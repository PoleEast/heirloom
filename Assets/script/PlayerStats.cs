using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int MaxHP;
    public int Damage;
    public float flashTime;
    [HideInInspector]
    public int currentHP;
    protected Rigidbody2D myRigidbody;
    protected Animator myAnim;
    protected BoxCollider2D myFeet;
    protected CapsuleCollider2D mybody;
    protected bool oriented;    //true=R false=f
    private GameObject PlayerHPBar;

    void Start()
    {

        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        mybody = GetComponent<CapsuleCollider2D>();
        PlayerHPBar = GameObject.Find("PlayerBar");
        myAnim.SetBool("idle", true);
        currentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        fall();
        fallEnd();
        die();
    }
    void LateUpdate()
    {
        flip();
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
        if (currentHP <= 0 && !(myAnim.GetBool("die")))
        {
            myAnim.SetBool("die", true);
            AbleToControl(false);
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
        currentHP = currentHP - Damage;
        PlayerHPBar.GetComponent<HPControl>().UpDateHPText(currentHP);
    }
    void moveforWard(float Xspeed, float Yspeed)
    {
        Vector2 playerVel = new Vector2(Xspeed, Yspeed);
        myRigidbody.velocity = playerVel;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHitbox" && !(myAnim.GetBool("die")))
        {
            myAnim.SetTrigger("hit");
            AbleToControl(false);
            myRigidbody.velocity = new Vector2(1.0f, 1.0f);
            StartCoroutine(IwaitforSec(0.2f));
        }
    }
    void AbleToControl(bool whether)
    {
        GetComponent<PlayerMove>().enabled = whether;
        GetComponent<PlayAttack>().enabled = whether;
    }
    IEnumerator IwaitforSec(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (currentHP > 0)
        {
            AbleToControl(true);
        }
    }
}

