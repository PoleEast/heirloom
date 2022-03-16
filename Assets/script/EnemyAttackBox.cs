using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAttackBox : MonoBehaviour
{
    private PolygonCollider2D hitbox;
    // Start is called before the first frame update
    public void Start()
    {
        hitbox = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame

    public void attack()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int Damage = gameObject.transform.parent.gameObject.GetComponent<Enemy>().Damage;
            GameObject.Find("Player").GetComponent<PlayerStats>().TakeDamage(Damage);
        }
    }
}

