using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public void TakeDamage(Enemy enemy, SpriteRenderer spriteRenderer)
    {
        Color originalColor = new Color();
        enemy.HP = enemy.HP - enemy.Damage;
        originalColor = spriteRenderer.color;
        Debug.Log(spriteRenderer.color);
        spriteRenderer.color = Color.red;
        StartCoroutine(resetcolor(enemy.flashTime, originalColor, spriteRenderer));
    }
    IEnumerator resetcolor(float flashTime, Color originalColor, SpriteRenderer spriteRenderer)
    {
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.color = originalColor;
        Debug.Log(spriteRenderer.color);
    }
}
