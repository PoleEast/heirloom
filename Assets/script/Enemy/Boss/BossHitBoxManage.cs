using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossHitBoxManage : MonoBehaviour
{
    public float skillDisTime;
    public float Attack_1DisTime;
    public float Attack_2DisTime;
    public GameObject skill;
    public GameObject Attack_1;
    public GameObject Attack_2;
    private PolygonCollider2D Attack_1HitBox;
    private PolygonCollider2D Attack_2HitBox;
    private PolygonCollider2D SkillHitBox;

    protected virtual void Start()
    {
        Attack_1HitBox = Attack_1.GetComponent<PolygonCollider2D>();
        Attack_2HitBox = Attack_2.GetComponent<PolygonCollider2D>();
        SkillHitBox = skill.GetComponent<PolygonCollider2D>();
    }


    void Attack_1HitBoxON()
    {
        if (Attack_1HitBox != null)
        {
            Attack_1HitBox.enabled = true;
            StartCoroutine(disActiveHitBox(Attack_1DisTime, Attack_1HitBox));
        }
    }
    void Attack_2HitBoxON()
    {
        if (Attack_2HitBox != null)
        {
            Attack_2HitBox.enabled = true;
            StartCoroutine(disActiveHitBox(Attack_2DisTime, Attack_2HitBox));
        }
    }
    void SkillHitBoxOn()
    {
        if (SkillHitBox != null)
        {
            SkillHitBox.enabled = true;
            StartCoroutine(disActiveHitBox(skillDisTime, SkillHitBox));
        }
    }
    IEnumerator disActiveHitBox(float disTime, PolygonCollider2D hitBox)
    {
        yield return new WaitForSeconds(disTime);
        hitBox.enabled = false;
    }
}

