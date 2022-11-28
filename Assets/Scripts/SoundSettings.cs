using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    private static readonly string firstSetting = "firstSetting";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string BulletPref = "BulletPref";
    private int firstSettingInt;

    public Slider bgAudioSlider, bulletAudioSlider;
    public AudioSource bgAudioSource;
    public AudioSource[] bulletsAudioSource;

    private float bgFloat, bullFloat;

    void Start()
    {
        firstSettingInt = PlayerPrefs.GetInt(firstSetting);

        if(firstSettingInt == 0)
        {
            bgFloat = 0.5f;
            bullFloat = 0.5f;
            bgAudioSlider.value = bgFloat;
            bulletAudioSlider.value = bullFloat;
            PlayerPrefs.SetFloat(BackgroundPref, bgFloat);
            PlayerPrefs.SetFloat(BulletPref, bullFloat);
            PlayerPrefs.SetInt(firstSetting, -1);
        }

        else
        {
            bgFloat = PlayerPrefs.GetFloat(BackgroundPref);
            bgAudioSlider.value = bgFloat;
            bullFloat = PlayerPrefs.GetFloat(BulletPref);
            bulletAudioSlider.value = bullFloat;
        }
        
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(BackgroundPref, bgAudioSlider.value);
        PlayerPrefs.SetFloat(BulletPref, bulletAudioSlider.value);
    }

    public void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            Save();
        }
    }

    public void UpdateAudio()
    {
        bgAudioSource.volume = bgAudioSlider.value;

        for (int i = 0; i < bulletsAudioSource.Length; i++)
        {
            bulletsAudioSource[i].volume = bulletAudioSlider.value;
        }
    }
}
