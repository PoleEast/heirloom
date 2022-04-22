using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttack : MonoBehaviour
{
    private Animator myAnim;
    private Rigidbody2D myRigidbody;
    public float attackmove;
    private PlayerStats playerStats;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }
    void Update()
    {
        attack();
    }
    void attack()
    {
        if (Input.GetButtonDown("attack"))
        {
            if ((myAnim.GetBool("idle") || myAnim.GetBool("run")) && playerStats.checkGround())
            {
                myRigidbody.velocity = new Vector2(0.0f, 0.0f);
                myAnim.SetBool("attack", true);
                myAnim.SetBool("idle", false);
                StartCoroutine(IAttackWork());
            }
            //else if
        }
    }
    protected IEnumerator IAttackWork()
    {
        yield return new WaitForSeconds(0.2f);
        if (transform.localRotation.y == 0)
            myRigidbody.velocity = new Vector2(attackmove, 0.0f);
        else
            myRigidbody.velocity = new Vector2(attackmove * -1, 0.0f);
    }
    void animationAttackEven()
    {
        myAnim.SetBool("idle", true);
        myAnim.SetBool("attack", false);
    }
}
