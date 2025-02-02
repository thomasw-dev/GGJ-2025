using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameStart : MonoBehaviour
{
    public AudioMixer audioMixer;
    public UnityEvent GameStarted;

    void Start()
    {
        InitVolumeSettings();
        GameStarted.Invoke();
    }

    private void InitVolumeSettings()
    {
        Debug.Log("Volume Set");
        audioMixer.SetFloat(Global.MasterVolumeKey, Global.GetMasterVolume());
        audioMixer.SetFloat(Global.MusicVolumeKey, Global.GetMusicVolume());
        audioMixer.SetFloat(Global.SFXVolumeKey, Global.GetSFXVolume());
    }

}
