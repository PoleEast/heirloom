using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFlash : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    void SkillHitBoxON()
    {
        polygonCollider2D.enabled = true;
    }
    void SkillHitBoxOff()
    {
        polygonCollider2D.enabled = false;
    }
    void deleteGB()
    {
        Destroy(gameObject);
    }
}
