using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static bool isGamePaused = false;
    public static bool useTouchInput = false;

    public const string MasterVolumeKey = "MasterVolume";
    public const string MusicVolumeKey = "MusicVolume";
    public const string SFXVolumeKey = "SFXVolume";


    // Title font size - 128
    // Button font size - 48

    // Master

    public static float GetMasterVolume()
    {
        return PlayerPrefs.HasKey(MasterVolumeKey) ? PlayerPrefs.GetFloat(MasterVolumeKey) : 0f;
    }

    public static void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, value);
    }

    // Music

    public static float GetMusicVolume()
    {
        return PlayerPrefs.HasKey(MusicVolumeKey) ? PlayerPrefs.GetFloat(MusicVolumeKey) : 0f;
    }

    public static void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
    }

    // SFX

    public static float GetSFXVolume()
    {
        return PlayerPrefs.HasKey(SFXVolumeKey) ? PlayerPrefs.GetFloat(SFXVolumeKey) : 0f;
    }

    public static void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat(SFXVolumeKey, value);
    }
}
