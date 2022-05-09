using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public Vector2 PlayerPosition;
    public GameObject floatPoint;
    public GameObject EnemyHPControl;
    void Start()
    {

    }
    void Update()
    {

    }
    public void TakeDamage(Enemy enemy, SpriteRenderer spriteRenderer)
    {
        var Dmage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Damage;
        GameObject gb = Instantiate(floatPoint, enemy.transform.position, Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = Dmage.ToString();
        Color originalColor = new Color();
        enemy.CurrentHP = enemy.CurrentHP - Dmage;
        originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        StartCoroutine(resetcolor(enemy.flashTime, originalColor, spriteRenderer));
        EnemyHPControl.GetComponent<EnemyHPControl>().upDateHPBar(enemy.gameObject);
    }
    IEnumerator resetcolor(float flashTime, Color originalColor, SpriteRenderer spriteRenderer)
    {
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.color = originalColor;
    }
}
