using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource, effectSource;
    public static SoundManager instance;
    public Camera mainCamera;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    
    private void FixedUpdate()
    {
        transform.position = mainCamera.transform.position;
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
}
