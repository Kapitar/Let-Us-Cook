using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject settingsMenu; // Reference to the Settings Menu panel
    public Slider musicVolumeSlider; // Reference to the music volume slider
    public Slider SFXVolumeSlider; // Reference to the SFX volume slider
    public AudioSource musicSource; // Reference to the music AudioSource
    public AudioSource SFXSource; // Reference to the music AudioSource


    // Method to toggle the visibility of the settings menu
    public void ToggleSettingsMenu() {
        if (settingsMenu == null) {
            Debug.LogError("Settings Menu is not assigned!");
            return;
        }

        // Toggle the active state of the settings menu
        settingsMenu.SetActive(!settingsMenu.activeSelf);

        // Optional: Log the current state for debugging
        Debug.Log("Settings menu is now " + (settingsMenu.activeSelf ? "visible" : "hidden"));
    }

    private void Start() {
        if (musicVolumeSlider != null) {
            musicVolumeSlider.value = AudioListener.volume;
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (SFXVolumeSlider != null){ 
            SFXVolumeSlider.value = AudioListener.volume;
            SFXVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetMusicVolume(float volume) {
        if (musicSource != null) {
            musicSource.volume = volume;
            Debug.Log("Music volume set to: " + volume);
        }
    }

    public void SetSFXVolume(float volume) {
        if (SFXSource != null) {
            SFXSource.volume = volume;
            Debug.Log("SFX volume set to: " + volume);
        }
    }
}
