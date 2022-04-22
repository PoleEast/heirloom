using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float flashTime;
    public int HP;
    public int Damage;
    public GameObject Hitbox;
    public float attackrange;

    public float EnemyVisualField;
    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;
    protected Animator myAnim;
    protected Rigidbody2D myRigidbody;
    abstract protected void attack();
    abstract protected void move(Collider2D Player);


    Collider2D IsPlayerview()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position, EnemyVisualField, LayerMask.GetMask("Player"));
    }
    //abstract public void die();

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        move(IsPlayerview());
        Die();
    }
    protected void Flip(bool direction)   //true=R false=L
    {
        if (direction == true)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == false)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    protected void Die()
    {
        if (HP <= 0)
        {
            myAnim.SetBool("die", true);
            Hitbox.SetActive(false);
            Destroy(gameObject, 3);
        }
    }
}
