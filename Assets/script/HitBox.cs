using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int Damage;
    public float STime, ETime;
    private Animator myAnim;
    private PolygonCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        hitbox = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        attack();
    }
    void attack()
    {
        if (Input.GetButtonDown("attack") && myAnim.GetCurrentAnimatorStateInfo(0).IsName("sword") == false)
        {
            myAnim.SetTrigger("attack");
            StartCoroutine(enablehitbox());
        }
    }

    IEnumerator disablehitbox()
    {
        yield return new WaitForSeconds(ETime);
        hitbox.enabled = false;
    }
    IEnumerator enablehitbox()
    {
        yield return new WaitForSeconds(STime);
        hitbox.enabled = true;
        StartCoroutine(disablehitbox());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(" ");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }
}
