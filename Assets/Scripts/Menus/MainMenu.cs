using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer soundsMixer;

    void Start() {
        LoadVolumeValues();
    }

    // Init the the volume values with the saved values
    private void LoadVolumeValues() {
        float musicVolume = PlayerPrefs.GetFloat("musicVolume");
        musicMixer.SetFloat("musicVolume", musicVolume);
        float soundsVolume = PlayerPrefs.GetFloat("soundsVolume");
        soundsMixer.SetFloat("soundsVolume", soundsVolume);
    }

    
}
