using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp;
    public int Damage;
    public float flashTime;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    // Start is called before the first frame update
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Debug.Log("st");
    }

    // Update is called once per frame
    public void Update()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        Hp = Hp - damage;
        flashColor(flashTime);
    }
    void flashColor(float time)
    {
        spriteRenderer.color = Color.red;
        Invoke("resetcolor", time);
    }
    void resetcolor()
    {
        spriteRenderer.color = originalColor;
    }
}
