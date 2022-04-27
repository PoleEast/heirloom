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
    private BoxCollider2D myFeet;
    abstract protected void attack(Collider2D Player);
    abstract protected void move(Collider2D Player);

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
    }
    protected virtual void Update()
    {
        StateCheese();
    }

    Collider2D IsPlayerview()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position, EnemyVisualField, LayerMask.GetMask("Player"));
    }
    Collider2D IsAttackRange()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position, attackrange, LayerMask.GetMask("Player"));
    }

    void StateCheese()
    {
        if (HP >= 0)
        {
            if (checkGound())
                if (IsAttackRange())
                {
                    attack(IsAttackRange());
                }
                else
                    move(IsPlayerview());
        }
        else
            Die();
    }

    protected bool checkGound()
    {
        return myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
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
        myAnim.SetBool("die", true);
        Hitbox.SetActive(false);
        Destroy(gameObject, 3);
    }
}
