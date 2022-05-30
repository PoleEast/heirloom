using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float flashTime;
    public int MaxHP;
    public GameObject Hitbox;
    public float attackrange;

    public float EnemyVisualField;
    [HideInInspector]
    public int CurrentHP;
    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;
    protected Animator myAnim;
    protected Rigidbody2D myRigidbody;
    protected Bounds GroundLocation;
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
        CurrentHP = MaxHP;
    }
    protected virtual void Update()
    {
        LockPostion();
    }

    protected Collider2D IsPlayerview()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position, EnemyVisualField, LayerMask.GetMask("Player"));
    }
    protected Collider2D IsAttackRange()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position, attackrange, LayerMask.GetMask("Player"));
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
    void LockPostion()
    {
        if (transform.position.x < GroundLocation.min.x)
        {
            Vector3 vector3 = new Vector3();
            vector3.x = GroundLocation.min.x;
            vector3.y = transform.position.y;
            transform.position = vector3;
        }
        else if (transform.position.x > GroundLocation.max.x)
        {
            Vector3 vector3 = new Vector3();
            vector3.x = GroundLocation.max.x;
            vector3.y = transform.position.y;
            transform.position = vector3;
        }
    }
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            GroundLocation = other.collider.bounds;
        }
    }
    protected void Die()
    {
        myAnim.SetBool("die", true);
        Hitbox.SetActive(false);
        Destroy(gameObject, 2);
    }
}
