using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSettings audioSettings;
    public AudioMixer audioMixer;

    private void Start()
    {
        audiosetinn();
    }

    public void audiosetinn()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(audioSettings.masterVolume) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(audioSettings.musicVolume) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(audioSettings.sfxVolume) * 20);
    }

    public void SetMasterVolume(float volume)
    {
        audioSettings.SetMasterVolume(volume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioSettings.SetMusicVolume(volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioSettings.SetSFXVolume(volume);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
