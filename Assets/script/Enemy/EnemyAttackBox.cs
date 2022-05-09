using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAttackBox : MonoBehaviour
{
    public int Damage;
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerStats>().TakeDamage(Damage);
        }
    }
}

