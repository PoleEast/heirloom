using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxManger : MonoBehaviour
{
    public float Sword1DisTime;
    public float Sword2DisTime;
    public float Sword3DisTime;
    public float JumpSwordTime;
    public GameObject Sword1;
    public GameObject Sword2;
    public GameObject Sword3;
    public GameObject JumpSword;
    private PolygonCollider2D sword1HitBox;
    private PolygonCollider2D sword2HitBox;
    private PolygonCollider2D sword3HitBox;
    private PolygonCollider2D jumpSword;

    protected virtual void Start()
    {
        sword1HitBox = Sword1.GetComponent<PolygonCollider2D>();
        sword2HitBox = Sword2.GetComponent<PolygonCollider2D>();
        sword3HitBox = Sword3.GetComponent<PolygonCollider2D>();
        jumpSword = JumpSword.GetComponent<PolygonCollider2D>();
    }
    void Sword1HitBoxON()
    {
        if (sword1HitBox != null)
        {
            sword1HitBox.enabled = true;
            StartCoroutine(disActiveHitBox(Sword1DisTime, sword1HitBox));
        }
    }
    void Sword2HitBoxON()
    {
        if (sword2HitBox != null)
        {
            sword2HitBox.enabled = true;
            StartCoroutine(disActiveHitBox(Sword2DisTime, sword2HitBox));
        }
    }
    void Sword3HitBoxON()
    {
        if (sword3HitBox != null)
        {
            sword3HitBox.enabled = true;
            StartCoroutine(disActiveHitBox(Sword3DisTime, sword3HitBox));
        }
    }
    void JumpSwordHitBoxON()
    {
        if (jumpSword != null)
        {
            jumpSword.enabled = true;
            StartCoroutine(disActiveHitBox(Sword3DisTime, jumpSword));
        }
    }
    IEnumerator disActiveHitBox(float disTime, PolygonCollider2D hitBox)
    {
        yield return new WaitForSeconds(disTime);
        hitBox.enabled = false;
    }
}
