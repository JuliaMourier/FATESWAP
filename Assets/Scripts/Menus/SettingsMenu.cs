using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer soundsMixer;
    public AudioMixer musicMixer;

    //Set the volume of the audio sources which have the mixer musicMixer
    public void SetVolumeMusic(float volume){
        musicMixer.SetFloat("musicVolume", volume);
    }

    //Set the volume of the audio sources which have the mixer soundMixer
    public void SetVolumeSounds(float volume){
        soundsMixer.SetFloat("soundsVolume", volume);
    }
}
