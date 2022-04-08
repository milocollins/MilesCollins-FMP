using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer AM;
    public AudioMixerSnapshot gameplay;
    public AudioMixerSnapshot ui;
    public AudioMixerGroup SFX;
    public AudioMixerGroup Music;
    public AudioMixerGroup UI;
    public AudioMixerGroup Master;
    public float masterVolume = 0f;
    private string[] amgVolumes = { "MasterVolume", "SFXVolume", "MusicVolume", "UIVolume" };
    
    public AudioClip[] musicList;
    public GameObject musicLoop;
    public GameObject pauseMusic;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SFXManager.instance.LoopMusic(false, false, false);
    }
    public static void TransitionToSnapshot(AudioMixerSnapshot ams, float t)
    {
        ams.TransitionTo(t);
    }
    public void SetVolume(float f, string amg)
    {
        /*
         * f will be 0 to 100.
         * float "volume" is the value converted to decibels which ranges -80 to 20.
        */
        if (amgVolumes.Contains(amg))
        {
            float volume = f - 80;
            AM.SetFloat(amg, volume);
        }
        else
        {
            Debug.Log("Invalid AMG");
        }
    }
    public float GetVolume(string amg)
    {
        float currentVolume = 0;
        if (amgVolumes.Contains(amg))
        {
            AM.GetFloat(amg, out currentVolume);
            currentVolume += 80;
        }
        return currentVolume;
    }
    public void SetMusicLoop(GameObject GO)
    {
        if (musicLoop!=null)
        {
            Destroy(musicLoop);
        }
        musicLoop = GO;
    }
    public void SetPauseMusic(GameObject GO, bool b)
    {
        if (b)
        {
            musicLoop.GetComponent<AudioSource>().Pause();
            pauseMusic = GO;
        }
        else
        {
            Destroy(pauseMusic);
            musicLoop.GetComponent<AudioSource>().Play();
        }
    }
}
