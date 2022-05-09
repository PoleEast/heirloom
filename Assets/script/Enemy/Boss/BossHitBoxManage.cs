using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossHitBoxManage : MonoBehaviour
{
    public float skillDisTime;
    public float Attack_1DisTime;
    public GameObject skill;
    public GameObject Attack_1;
    private PolygonCollider2D Attack_1HitBox;
    private PolygonCollider2D SkillHitBox;

    protected virtual void Start()
    {
        Attack_1HitBox = Attack_1.GetComponent<PolygonCollider2D>();
        SkillHitBox=skill.GetComponent<PolygonCollider2D>();
    }


    void Attack_1HitBoxON()
    {
        if (Attack_1HitBox != null)
        {
            Attack_1HitBox.enabled = true;
            StartCoroutine(disActiveHitBox(Attack_1DisTime, Attack_1HitBox));
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

