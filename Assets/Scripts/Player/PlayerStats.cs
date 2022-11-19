using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
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
}
