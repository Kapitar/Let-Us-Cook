using UnityEngine;

public class SFXSound : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
        }
    }

    public void playOnClick() {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
