using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI damageUpgradeCost;
    public TextMeshProUGUI healthUpgradeCost;
    public TextMeshProUGUI fireSpeedUpgradeCost;

    private int dmgUpgradeLvl = 0;
    private int hpUpgradeLvl = 0;
    private int fsUpgradeLvl = 0;

    private float dmgUpgradeCost = 50;
    private float hpUpgradeCost = 10;
    private float fsUpgradeCost = 10;

    public float dmgMult = 1.2f;
    public float hpMult = 1.2f;
    public float fsMult = 1.2f;

    private float increment = 1.1f;


    public TextMeshProUGUI moneyText;
    public float damageMult { get; private set; }
    public float healthMult { get; private set; }
    public float fireSpeedMult { get; private set; }
    public float money  { get; private set; }

    public static PlayerStats _instance { get; private set; }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        money = 0;
        healthMult = 1;
        fireSpeedMult = 1;
        damageMult = 1;
    }

    private void Update()
    {
        moneyText.text = money.ToString();
    }

    public void AddMoney(float value)
    {
        money += value;
        moneyText.text = money.ToString();
    }
    public void HealthUpgrade()
    {
        healthMult *= 1.3f;
    }
    public void FireSpeedUpgrade()
    {
        fireSpeedMult *= 1.3f;
    }
    public void DamageUpgrade()
    {
        damageMult *= 1.3f;
    }


    public void DmgUpgradee()
    {

        if (dmgUpgradeCost < money)
        {
            money -= dmgUpgradeCost;
            dmgUpgradeLvl++;
            dmgMult = dmgMult * increment;
            dmgUpgradeCost = dmgUpgradeCost * increment;
            damageUpgradeCost.text = dmgUpgradeCost.ToString();
        }

        else
        {
            Debug.Log("si chudobny cavo");
        }

    }
}
