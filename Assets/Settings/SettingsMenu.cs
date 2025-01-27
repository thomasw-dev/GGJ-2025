using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;


    public void OnEnable()
    {
        GetVolume();
    }

    public void GetVolume()
    {
        masterSlider.value = Global.GetMasterVolume();
        musicSlider.value = Global.GetMusicVolume();
        sfxSlider.value = Global.GetSFXVolume();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(Global.MasterVolumeKey, volume);
        Global.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(Global.MusicVolumeKey, volume);
        Global.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(Global.SFXVolumeKey, volume);
        Global.SetSFXVolume(volume);
    }
}
