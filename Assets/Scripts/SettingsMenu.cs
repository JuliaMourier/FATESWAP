using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer soundsMixer;
    public AudioMixer musicMixer;

    public void SetVolumeMusic(float volume){
        musicMixer.SetFloat("musicVolume", volume);
    }

    public void SetVolumeSounds(float volume){
        soundsMixer.SetFloat("soundsVolume", volume);
    }
}
