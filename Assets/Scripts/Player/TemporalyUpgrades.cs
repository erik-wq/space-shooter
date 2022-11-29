using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalyUpgrades : MonoBehaviour
{
    public float dmg = 0;
    public float hp = 0;
    public float speed = 0;
    
    private float dmgUp = .05f;
    private float hpUp = .05f;
    private float speedUp = .05f;
    
    public void Reset()
    {
        dmg = 0;
        hp = 0;
        speed = 0;
    }
    public void HpUp()
    {
        hp += hpUp;
    }
    public void DmgUp()
    {
        dmg += dmgUp;
    }
    public void SpeedUp()
    {
        speed += speedUp;
    }
}
