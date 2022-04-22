using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManger : MonoBehaviour
{
    public float Sword1DisTime;
    public float Sword2DisTime;
    public float Sword3DisTime;
    public GameObject Sword1;
    public GameObject Sword2;
    public GameObject Sword3;
    private PolygonCollider2D Sword1HitBox;
    private PolygonCollider2D Sword2HitBox;
    private PolygonCollider2D Sword3HitBox;

    void Start()
    {
        Sword1HitBox = Sword1.GetComponent<PolygonCollider2D>();
        Sword2HitBox = Sword2.GetComponent<PolygonCollider2D>();
        Sword3HitBox = Sword3.GetComponent<PolygonCollider2D>();
    }
    void Sword1HitBoxON()
    {
        Sword1HitBox.enabled = true;
        Debug.Log("Sword1" + Sword1HitBox.isActiveAndEnabled);
        StartCoroutine(disActiveHitBox(Sword1DisTime, Sword1HitBox));
    }
    void Sword2HitBoxON()
    {
        Sword2HitBox.enabled = true;
        Debug.Log("Sword2" + Sword2HitBox.isActiveAndEnabled);
        StartCoroutine(disActiveHitBox(Sword2DisTime, Sword2HitBox));
    }
    void Sword3HitBoxON()
    {
        Sword3HitBox.enabled = true;
        Debug.Log("Sword3" + Sword3HitBox.isActiveAndEnabled);
        StartCoroutine(disActiveHitBox(Sword3DisTime, Sword3HitBox));
    }
    IEnumerator disActiveHitBox(float disTime, PolygonCollider2D hitBox)
    {
        yield return new WaitForSeconds(disTime);
        hitBox.enabled = false;
    }
}
