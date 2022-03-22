using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPControl : MonoBehaviour
{
    public Text HPText;
    public static int HPcurrent;
    private static int HPMax;
    private Image hpBar;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = GetCoponent<Image>();
        HPMax = GameObject.Find("Player").GetComponent<PlayerStats>().MaxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
