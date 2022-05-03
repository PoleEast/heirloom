using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HitBoxManger : MonoBehaviour
{
    public float skillFlashDisTime;
    public GameObject skill;
    public float Attack_1DisTime;
    public GameObject Attack_1;
    private GameObject skillFlash;
    public List<PolygonCollider2D> LskillFlashHitBox;
    private PolygonCollider2D Attack_1HitBox;

    protected virtual void Start()
    {
        Attack_1HitBox = Attack_1.GetComponent<PolygonCollider2D>();
        GetskillFlashchilds();
    }

    void GetskillFlashchilds()
    {
        Debug.Log("Infunction");
        for (int i = 0; i < skill.transform.childCount; i++)
        {
            Debug.Log("GetChild");
            LskillFlashHitBox.Add(skill.transform.GetChild(i).gameObject.GetComponent<PolygonCollider2D>());
        }
    }
    void Sword1HitBoxON()
    {
        if (sword1HitBox != null)
        {
            sword1HitBox.enabled = true;
            StartCoroutine(disActiveHitBox(Sword1DisTime, sword1HitBox));
        }
    }
    IEnumerator disActiveHitBox(float disTime, PolygonCollider2D hitBox)
    {
        yield return new WaitForSeconds(disTime);
        hitBox.enabled = false;
    }
}

