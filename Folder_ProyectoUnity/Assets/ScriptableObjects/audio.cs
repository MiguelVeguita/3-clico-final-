using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Audio/AudioSettings")]
public class AudioSettings : ScriptableObject
{
    public float masterVolume = 1.0f;
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }
}
