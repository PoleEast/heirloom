using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float TakeDamageSwitchColorTime;
    public float TakeDamageCDTime;
    public int MaxHP;
    public int Damage;
    public float flashTime;
    [HideInInspector]
    public int currentHP;
    protected Rigidbody2D myRigidbody;
    private bool TakeDamageCD;
    protected Animator myAnim;
    protected BoxCollider2D myFeet;
    protected CapsuleCollider2D mybody;
    protected bool oriented;    //true=R false=f
    private GameObject PlayerHPBar;
    private SpriteRenderer mySpriteRenderer;
    private Color originalColor;

    void Start()
    {
        TakeDamageCD = false;
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        mybody = GetComponent<CapsuleCollider2D>();
        PlayerHPBar = GameObject.Find("PlayerBar");
        myAnim.SetBool("idle", true);
        currentHP = MaxHP;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = mySpriteRenderer.color;
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
            Destroy(this);
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
        if (!(myAnim.GetBool("roll")) && !TakeDamageCD)
        {
            currentHP = currentHP - Damage;
            PlayerHPBar.GetComponent<HPControl>().UpDateHPText(currentHP);
            StartCoroutine(IwaitforSec(TakeDamageCDTime));
            hit();
            TakeDamageCD = true;
            StartCoroutine(ITakeDamageSwitchColor());
            Debug.Log("a");
        }
    }
    void moveforWard(float Xspeed, float Yspeed)
    {
        Vector2 playerVel = new Vector2(Xspeed, Yspeed);
        myRigidbody.velocity = playerVel;
    }
    void hit()
    {
        myAnim.SetTrigger("hit");
        AbleToControl(false);
        if (oriented)
            myRigidbody.velocity = new Vector2(1.0f, 1.0f);
        else
            myRigidbody.velocity = new Vector2(-1.0f, 1.0f);
        StartCoroutine(IwaitforSec(0.2f));
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
    IEnumerator ITakeDamageCD(float sec)
    {
        TakeDamageCD = false;
        yield return new WaitForSeconds(sec);
        TakeDamageCD = true;
    }
    IEnumerator ITakeDamageSwitchColor()
    {
        for (int i = 0; i < 3; i++)
        {
            mySpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.3f);
            mySpriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.3f);
        }
        TakeDamageCD = false;
    }
}

