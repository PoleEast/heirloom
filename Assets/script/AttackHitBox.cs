using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackHitBox : MonoBehaviour
{
    public float STime, ETime;
    private Animator myAnim;
    private PolygonCollider2D hitbox;
    // Start is called before the first frame update
    public void Start()
    {
        myAnim = gameObject.transform.parent.gameObject.GetComponent<Animator>();
        hitbox = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame

    public void attack()
    {
        StartCoroutine(enablehitbox());
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
            GameObject.Find("GameControl").GetComponent<GameControl>().TakeDamage(collision.gameObject.GetComponent<Enemy>(), spriteRenderer);
        }
    }
}

