using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackHitBox : MonoBehaviour
{
    private PolygonCollider2D hitbox;
    // Start is called before the first frame update
    public void Start()
    {
        hitbox = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            GameObject.Find("GameControl").GetComponent<GameControl>().TakeDamage(collision.gameObject.GetComponent<Enemy>(), spriteRenderer);
        }
    }
}

