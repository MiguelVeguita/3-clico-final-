using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour
{
    public AudioManager audioManager;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        masterSlider.value = audioManager.audioSettings.masterVolume;
        musicSlider.value = audioManager.audioSettings.musicVolume;
        sfxSlider.value = audioManager.audioSettings.sfxVolume;

        masterSlider.onValueChanged.AddListener(audioManager.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(audioManager.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(audioManager.SetSFXVolume);
    }
}
