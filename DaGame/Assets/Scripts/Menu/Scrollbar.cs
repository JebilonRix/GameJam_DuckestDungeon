using UnityEngine;
using UnityEngine.Audio;

public class Scrollbar : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("musicVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("sfxVolume", volume);
    }
}