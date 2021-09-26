using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixerSnapshot _notWalking;
    [SerializeField] private AudioMixerSnapshot _narration;
    [SerializeField] private AudioSource[] source;
    [SerializeField] private AudioSource _takeMemeSource;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        source[0].Play();
        source[1].Stop();
        source[2].Stop();
    }
    public void ChangeTheme(int index)
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (i == index)
            {
                source[index].Play();
            }
            else
            {
                source[i].Stop();
            }
        }
    }

    public void TakeMemeSound(AudioClip clip)
    {
        _takeMemeSource.PlayOneShot(clip);
        _narration.TransitionTo(2f);
        
        DOVirtual.DelayedCall(clip.length, () => _notWalking.TransitionTo(1f));
    }
    
    
}