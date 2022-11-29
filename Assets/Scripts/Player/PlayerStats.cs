using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    #region GUI
    public TextMeshProUGUI damageUpgradeCost;
    public TextMeshProUGUI healthUpgradeCost;
    public TextMeshProUGUI fireSpeedUpgradeCost;

    public TextMeshProUGUI currentDamageTxt;
    public TextMeshProUGUI currentHealthTxt;
    public TextMeshProUGUI currentFireSpeedTxt;

    public TextMeshProUGUI damageAfterUpdate;
    public TextMeshProUGUI healthAfterUpdate;
    public TextMeshProUGUI fireSpeedAfterUpdate;
    #endregion

    #region Upgrades
    private float dmgUpgradeCost = 50;
    private float hpUpgradeCost = 50;
    private float fsUpgradeCost = 50;

    public float dmgMult = 1.2f;
    public float hpMult = 1.2f;
    public float fsMult = 1.2f;

    private float increment = 1.1f;
    #endregion
    private float baseDamage = 30;
    private float baseFireSpeed = 2f;
    private float baseHealth = 100;
    public float currentDamage {get; private set;}
    public float currentHealth {get; private set;}
    public float currentFireSpeed {get; private set;}
    #region  Stats
    #endregion

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

        currentDamage = baseDamage;
        currentHealth = baseHealth;
        currentFireSpeed = baseFireSpeed;
        money = 1000;
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
            currentDamage = baseDamage + ((baseDamage* dmgMult)-baseDamage);
            currentDamage = Mathf.Round(currentDamage * 10);
            currentDamage /= 10;
            damageUpgradeCost.text = dmgUpgradeCost.ToString();
            currentDamageTxt.text = currentDamage.ToString();
            damageAfterUpdate.text = ((int)(baseDamage * (increment * dmgMult))).ToString();
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
            currentHealth = baseHealth + ((baseHealth * hpMult) - baseHealth);
            currentHealth = Mathf.Round(currentHealth * 10);
            currentHealth /= 10;
            healthUpgradeCost.text = hpUpgradeCost.ToString();
            currentHealthTxt.text = currentHealth.ToString();
            healthAfterUpdate.text = ((int)(baseHealth * (increment * hpMult))).ToString();
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
            currentFireSpeed = baseFireSpeed / fsMult * 2;
            currentFireSpeed = Mathf.Round(currentFireSpeed * 10);
            currentFireSpeed /= 10;
            fireSpeedUpgradeCost.text = fsUpgradeCost.ToString();
            currentFireSpeedTxt.text = currentFireSpeed.ToString();
            fireSpeedAfterUpdate.text = ((int)(baseFireSpeed * (increment * fsMult))).ToString();
        }
    }
}
