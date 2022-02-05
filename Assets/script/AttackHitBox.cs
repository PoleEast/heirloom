using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public float STime, ETime;
    private Animator myAnim;
    private PolygonCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GameObject.FindGameObjectWithTag(gameObject.transform.parent.tag).GetComponent<Animator>();
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
        if (collision.gameObject.tag == "Enemy")
        {
            SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            Debug.Log(GameObject.FindGameObjectWithTag("Enemy").GetComponent<SpriteRenderer>().color);
            GameObject.Find("GameControl").GetComponent<GameControl>().TakeDamage(collision.gameObject.GetComponent<Enemy>(), spriteRenderer);
        }
    }
}
