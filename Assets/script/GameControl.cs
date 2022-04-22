using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public Vector2 PlayerPosition;
    public GameObject floatPoint;
    void Start()
    {

    }
    void Update()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    public void TakeDamage(Enemy enemy, SpriteRenderer spriteRenderer)
    {
        var Dmage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Damage;
        GameObject gb = Instantiate(floatPoint, enemy.transform.position, Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = Dmage.ToString();
        Color originalColor = new Color();
        enemy.HP = enemy.HP - Dmage;
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
