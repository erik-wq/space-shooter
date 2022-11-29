using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private static readonly string firstSetting = "firstSetting";
    private static readonly string dmgUpgradeCostPref = "dmgUpgradeCostPref";
    private static readonly string hpUpgradeCostPref = "hpUpgradeCostPref";
    private static readonly string fsUpgradeCostPref = "fsUpgradeCostPref";

    private static readonly string dmgMultPref = "dmgMultPref";
    private static readonly string hpMultPref = "hpMultPref";
    private static readonly string fsMultPref = "fsMultPref";

    private static readonly string currentDamagePref = "currentDamagePref";
    private static readonly string currentHealthPref = "currentHealthv";
    private static readonly string currentFireSpeedPref = "currentFireSpeedPref";

    public PlayerStats playerStats;

    private int firstSettingInt;

    void Start()
    {
        firstSettingInt = PlayerPrefs.GetInt(firstSetting);

        if (firstSettingInt == 0)
        {

            //float nieco = PlayerStats._instance.currentDamage; 

            PlayerPrefs.SetFloat(dmgUpgradeCostPref, 50);
            PlayerPrefs.SetFloat(hpUpgradeCostPref, 50);
            PlayerPrefs.SetFloat(fsUpgradeCostPref, 50);

            PlayerPrefs.SetFloat(dmgMultPref, 1.2f);
            PlayerPrefs.SetFloat(hpMultPref, 1.2f);
            PlayerPrefs.SetFloat(fsMultPref, 1.2f);

            PlayerPrefs.SetInt(firstSetting, -1);
        }

        else
        {
            PlayerPrefs.GetFloat(dmgUpgradeCostPref);
            PlayerPrefs.GetFloat(hpUpgradeCostPref);
            PlayerPrefs.GetFloat(fsUpgradeCostPref);

            PlayerPrefs.GetFloat(dmgMultPref);
            PlayerPrefs.GetFloat(hpMultPref);
            PlayerPrefs.GetFloat(fsMultPref);
        }
    }


    public void SaveGame()
    {
        PlayerPrefs.SetFloat(dmgUpgradeCostPref, playerStats.dmgUpgradeCost);
        PlayerPrefs.SetFloat(hpUpgradeCostPref, playerStats.hpUpgradeCost);
        PlayerPrefs.SetFloat(fsUpgradeCostPref, playerStats.fsUpgradeCost);

        PlayerPrefs.SetFloat(dmgMultPref, playerStats.dmgMult);
        PlayerPrefs.SetFloat(hpMultPref, playerStats.hpMult);
        PlayerPrefs.SetFloat(fsMultPref, playerStats.fsMult);
    }




    public void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveGame();
        }
    }
}
