using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPControl : MonoBehaviour
{
    public Text HPText;
    public UnityEngien.UI.Image HpBar;

    [HideInInspector]
    public int HPcurrent;
    private static int MaxHP;
    // Start is called before the first frame update
    void Start()
    {
        MaxHP = GameObject.Find("Player").GetComponent<PlayerStats>().MaxHP;
        HPText.GetComponent<Text>().text = MaxHP + "/" + MaxHP;
    }

    // Update is called once per frame

    public void UpDateHPText(int currentHP)
    {
        HPText.GetComponent<Text>().text = currentHP + "/" + MaxHP;
        upDateHPBar(currentHP);
    }

    void upDateHPBar(int currentHP)
    {
        float HpPC = currentHP / MaxHP;
        HpBar.fillamount = HpPC;
    }
}
