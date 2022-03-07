using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public Vector2 PlayerPosition;
    void Start()
    {

    }
    void Update()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    public void TakeDamage(Enemy enemy, SpriteRenderer spriteRenderer)
    {
        Color originalColor = new Color();
        enemy.HP = enemy.HP - enemy.Damage;
        originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        StartCoroutine(resetcolor(enemy.flashTime, originalColor, spriteRenderer));
    }
    IEnumerator resetcolor(float flashTime, Color originalColor, SpriteRenderer spriteRenderer)
    {
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.color = originalColor;
    }
}
