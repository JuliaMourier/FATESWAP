using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // String constants
    private const string MUSIC_VOLUME = "musicVolume";
    private const string SOUNDS_VOLUME = "soundsVolume";

    // Audio
    public AudioMixer musicMixer;
    public AudioMixer soundsMixer;
    public AudioSource sound;

    // Sliders
    public Slider musicSlider;
    public Slider soundsSlider;

    private bool isInitialized = false;

    void Start() {
        LoadValues();
        isInitialized = true;
    }

    //Set the volume of the audio sources which have the mixer musicMixer
    public void SetVolumeMusic(float volume) {
        musicMixer.SetFloat(MUSIC_VOLUME, volume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
    }

    //Set the volume of the audio sources which have the mixer soundMixer
    public void SetVolumeSounds(float volume) {
        soundsMixer.SetFloat(SOUNDS_VOLUME, volume);
        PlayerPrefs.SetFloat(SOUNDS_VOLUME, volume);
        if (isInitialized) {
            sound.Play();
        }
    }

    // Load the saved volume values
    private void LoadValues() {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME);
        musicMixer.SetFloat(MUSIC_VOLUME, musicVolume);
        musicSlider.value = musicVolume;
        
        float soundsVolume = PlayerPrefs.GetFloat(SOUNDS_VOLUME);
        soundsMixer.SetFloat(SOUNDS_VOLUME, soundsVolume);
        soundsSlider.value = soundsVolume;
    }
}
