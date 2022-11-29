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

    public TextMeshProUGUI currentDamageTxt;
    public TextMeshProUGUI currentHealthTxt;
    public TextMeshProUGUI currentFireSpeedTxt;

    public TextMeshProUGUI damageAfterUpdate;
    public TextMeshProUGUI healthAfterUpdate;
    public TextMeshProUGUI fireSpeedAfterUpdate;



    private float dmgUpgradeCost = 50;
    private float hpUpgradeCost = 50;
    private float fsUpgradeCost = 50;

    public float dmgMult = 1.2f;
    public float hpMult = 1.2f;
    public float fsMult = 1.2f;

    private float increment = 1.1f;

    private PlayerMovement playerMovement;

    public TextMeshProUGUI moneyText;
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
        hpMult = 1;
        fsMult = 1;
        dmgMult = 1;
    }

    private void Update()
    {
        moneyText.text = money.ToString();
    }

    public void AddMoney(float value)
    {
        money += value;
        money = (int)(money);
        moneyText.text = money.ToString();
    }

    public void RemoveMoney(float value)
    {
        money -= value;
        money = (int)(money);
        moneyText.text = money.ToString();
    }


    public void DmgUpgrade()
    {

        if (dmgUpgradeCost < money)
        {
            money -= dmgUpgradeCost;
            dmgMult *= increment;
            dmgUpgradeCost *= increment;
            dmgUpgradeCost = (int)dmgUpgradeCost;
            damageUpgradeCost.text = dmgUpgradeCost.ToString();
        }
    }

    public void HealthUpgrade()
    {

        if (hpUpgradeCost < money)
        {
            
            money -= dmgUpgradeCost;
            hpMult *= increment;
            hpUpgradeCost *= increment;
            hpUpgradeCost = (int)hpUpgradeCost;
            healthUpgradeCost.text = hpUpgradeCost.ToString();
        }

    }

    public void FireSpeedUpgrade()
    {

        if (dmgUpgradeCost < money)
        {
            money -= dmgUpgradeCost;
            fsMult *= increment;
            fsUpgradeCost *= increment;
            fsUpgradeCost = (int)fsUpgradeCost;
            fireSpeedUpgradeCost.text = fsUpgradeCost.ToString();
        }
    }
}
