using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public GameObject sfxPrefab;

    private void Awake()
    {
        instance = this;
    }
    public void PlaySFX(AudioClip ac)
    {
        StartCoroutine(SFX(ac));
    }
    private IEnumerator SFX(AudioClip ac)
    {
        GameObject GO = Instantiate(sfxPrefab);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = ac;
        AS.Play();
        yield return new WaitForSeconds(ac.length);
        Destroy(GO);
    }
    public void PlaySFX(AudioClip ac, Transform position, Vector2 minMax)
    {
        StartCoroutine(SFX(ac, position, minMax));
    }
    private IEnumerator SFX(AudioClip ac, Transform t, Vector2 v)
    {
        GameObject GO = Instantiate(sfxPrefab, t.position, Quaternion.identity);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = ac;
        AS.spatialBlend = 1f;
        AS.minDistance = v.x;
        AS.maxDistance = v.y;
        AS.Play();
        yield return new WaitForSeconds(ac.length);
        Destroy(GO);
    }
    public void PlayOneShot(AudioClip ac, AudioMixerGroup amg)
    {
        StartCoroutine(OneShot(ac, amg));
    }
    private IEnumerator OneShot(AudioClip ac, AudioMixerGroup amg)
    {
        GameObject GO = Instantiate(sfxPrefab);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = ac;
        AS.outputAudioMixerGroup = amg;
        AS.Play();
        yield return new WaitForSeconds(ac.length);
        Destroy(GO);
    }
}
