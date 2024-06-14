using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioSource musicSource;

    public Toggle volumeToggle;

    [Header("Clips")]
    public AudioClip click;
    public AudioClip collect;
    public AudioClip death;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Allow Volume", 1) == 0)
            volumeToggle.isOn = false;
        else
            volumeToggle.isOn = true;

        ToggleVolume();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(click);
    }

    public void PlayCollectSound()
    {
        audioSource.PlayOneShot(collect);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(death);
    }

    public void ToggleVolume()
    {
        int toggle = volumeToggle.isOn ? 1 : 0;
        if(toggle == 1)
        {
            audioSource.volume = toggle;
            musicSource.volume = 0.4f;
        }
        else
        {
            audioSource.volume = toggle;
            musicSource.volume = toggle;
        }
        
        PlayerPrefs.SetInt("Allow Volume", toggle);
    }
}
