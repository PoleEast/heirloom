using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyHPControl : MonoBehaviour
{
    public Image HpBar;

    [HideInInspector]
    public int HPcurrent;
    private static int MaxHP;
    private List<GameObject> EnemyList;
    // Start is called before the first frame update
    void Start()
    {
        EnemyList = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    }
    public void upDateHPBar(GameObject GB)
    {
        foreach (GameObject gb in EnemyList)
        {
            if (gb == GB)
            {
                float HpPC = (float)gb.GetComponent<Enemy>().CurrentHP / gb.GetComponent<Enemy>().MaxHP;
                Debug.Log(HpPC);
                HpBar.fillAmount = HpPC;
            }
        }
    }
}
