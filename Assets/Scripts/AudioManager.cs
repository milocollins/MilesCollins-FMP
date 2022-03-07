using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer AM;

    private void Awake()
    {
        instance = this;
    }
    public static void TransitionToSnapshot(AudioMixerSnapshot ams, float t)
    {
        ams.TransitionTo(t);
    }
}
