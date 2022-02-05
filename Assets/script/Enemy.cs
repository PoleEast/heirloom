using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : BaseNumber
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    abstract public void attack(int Damage);
    //abstract public void die();
    //abstract public void move();
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Debug.Log(spriteRenderer.color);
    }

    // Update is called once per frame
    public void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
