using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sound
{
    public enum name
    {
        BubblesPop1,
        BubblesPop2,
        BubblesPop3,
        MailError,
        MailShootsOut,
        MailSuccess,
        ShootBubbles,
        TruckEnters,
        TruckHurryUp,
        TruckShutsDoorLeaves
    }

    public static string AudioEnumToName(name name)
    {
        return name switch
        {
            name.BubblesPop1 => "bubblesPop_v1",
            name.BubblesPop2 => "bubblesPop_v2",
            name.BubblesPop3 => "bubblesPop_v3",
            name.MailError => "mailError",
            name.MailShootsOut => "mailShootsOut",
            name.MailSuccess => "mailSuccess",
            name.ShootBubbles => "shootBubbles",
            name.TruckEnters => "truckEnters",
            name.TruckHurryUp => "truckHurryUp",
            name.TruckShutsDoorLeaves => "truckShutsDoorLeaves",
            _ => ""
        };
    }
}

public class SFX : MonoBehaviour
{
    [Tooltip("Find the AudioSource component in its children and store them in this list.")]
    public List<AudioSource> audioList;

    void Start()
    {
        List<GameObject> audioObjects = new List<GameObject>();
        foreach (Transform child in transform)
        {
            audioObjects.Add(child.gameObject);
        }

            for (int i = 0; i < audioObjects.Count; i++)
        {
            AudioSource audioSource = audioObjects[i].GetComponent<AudioSource>();
            if (audioSource != null) audioList.Add(audioSource);
        }
    }

    public void Play(Sound.name name)
    {
        string audioName = Sound.AudioEnumToName(name);
        int index = AudioNameToIndex(audioName);
        audioList[index].PlayOneShot(audioList[index].clip);
    }

    public void Stop(Sound.name name)
    {
        string audioName = Sound.AudioEnumToName(name);
        int index = AudioNameToIndex(audioName);
        audioList[index].Stop();
    }

    int AudioNameToIndex(string name)
    {
        for (int i = 0; i < audioList.Count; i++)
        {
            if (audioList[i].gameObject.name == name)
                return i; // The index
        }
        return -1; // It is not in the list
    }
}