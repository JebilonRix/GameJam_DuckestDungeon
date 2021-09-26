using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] source;

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
}