using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public GameObject sfxPrefab;
    
    public AudioClip clickSFX;
    public AudioClip musicTest;
    public AudioClip sfxTest;

    public GameObject musicTestObj;
    public GameObject sfxTestObj;
    public GameObject uiTestObj;
    public GameObject masterTestObj;

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
        AS.dopplerLevel = 0;
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
    public void PlayOneShot(AudioClip ac, AudioMixerGroup amg, float time)
    {
        StartCoroutine(OneShot(ac, amg, time));
    }
    private IEnumerator OneShot(AudioClip ac, AudioMixerGroup amg, float time)
    {
        GameObject GO = Instantiate(sfxPrefab);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = ac;
        AS.outputAudioMixerGroup = amg;
        AS.Play();
        yield return new WaitForSeconds(time);
        Destroy(GO);
    }
    public void TestAMG(AudioMixerGroup amg)
    {
        if (amg == AudioManager.instance.Master)
        {
            StartCoroutine(OneShotTest(clickSFX, amg, clickSFX.length));
        }
        else if (amg == AudioManager.instance.Music)
        {
            StartCoroutine(OneShotTest(musicTest, amg, musicTest.length));
        }
        else if (amg == AudioManager.instance.UI)
        {
            StartCoroutine(OneShotTest(clickSFX, amg, clickSFX.length));
        }
        else if (amg == AudioManager.instance.SFX )
        {
            StartCoroutine(OneShotTest(sfxTest, amg, sfxTest.length));
        }
    }
    private IEnumerator OneShotTest(AudioClip ac, AudioMixerGroup amg, float time)
    {
        GameObject GO = Instantiate(sfxPrefab);
        
        if (amg == AudioManager.instance.Music)
        {
            musicTestObj = GO;
        }

        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = ac;
        AS.outputAudioMixerGroup = amg;
        AS.Play();
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        Debug.Log("Hit Time");
        if (amg == AudioManager.instance.Master)
        {
            masterTestObj = null;
        }
        else if (amg == AudioManager.instance.Music)
        {
            musicTestObj = null;
        }
        else if (amg == AudioManager.instance.UI)
        {
            uiTestObj = null;
        }
        else if (amg == AudioManager.instance.SFX)
        {
            sfxTestObj = null;
        }
        Destroy(GO);
    }
    public void PauseMusicToggle()
    {
        if (AudioManager.instance.pauseMusicGO != null)
        {
            if (AudioManager.instance.pauseMusicGO.activeInHierarchy)
            {
                Destroy(AudioManager.instance.pauseMusicGO);
            }
            AudioManager.instance.pauseMusicGO = null;
        }
        if (GameManager.instance.isPaused)
        {
            AudioManager.instance.musicLoopGO.GetComponent<AudioSource>().Pause();
            GameObject GO = Instantiate(sfxPrefab);
            AudioManager.instance.pauseMusicGO = GO;
            AudioSource AS = GO.GetComponent<AudioSource>();
            AS.playOnAwake = false;
            AS.loop = true;
            AS.outputAudioMixerGroup = AudioManager.instance.Music;
            AS.clip = AudioManager.instance.pauseMusic;
            AS.volume = AudioManager.instance.volumes[0];
            AS.Play();
        }
        else
        {
            Destroy(AudioManager.instance.pauseMusicGO);
            AudioManager.instance.pauseMusicGO = null;
            AudioManager.instance.musicLoopGO.GetComponent<AudioSource>().Play();
        }
    }
    public void MusicLoop(bool gameOver, bool gameWin)
    {
        if (AudioManager.instance.musicLoopGO != null)
        {
            if (AudioManager.instance.musicLoopGO.activeInHierarchy)
            {
                Destroy(AudioManager.instance.musicLoopGO);
            }
            AudioManager.instance.musicLoopGO = null;
        }
        GameObject GO = Instantiate(sfxPrefab);
        AudioManager.instance.musicLoopGO = GO;
        AudioSource AS = GO.GetComponent<AudioSource>();
        AS.playOnAwake = false;
        AS.loop = true;
        AS.outputAudioMixerGroup = AudioManager.instance.Music;
        if (gameOver)
        {
            AS.clip = AudioManager.instance.gameOverMusic;
            AS.volume = AudioManager.instance.volumes[1];
        }
        else if (gameWin)
        {
            AS.clip = AudioManager.instance.gameWinMusic;
            AS.volume = AudioManager.instance.volumes[2];
        }
        else
        {
            switch (SceneNavigator.instance.GetCurrentScene())
            {
                case "MainMenu":
                    AS.clip = AudioManager.instance.mainMenuMusic;
                    AS.volume = AudioManager.instance.volumes[3];
                    break;
                case "Prototype":
                    AS.clip = AudioManager.instance.gameplayMusic;
                    AS.volume = AudioManager.instance.volumes[4];
                    break;
                case "Level 2":
                    AS.clip = AudioManager.instance.gameplayMusic;
                    AS.volume = AudioManager.instance.volumes[4];
                    break;
                case "Level 3":
                    AS.clip = AudioManager.instance.gameplayMusic;
                    AS.volume = AudioManager.instance.volumes[4];
                    break;
                default:
                    break;
            }
        }
        AS.Play();
    }
    public void PlaySFX(AudioClip ac, Transform position, Vector2 minMax, float volume)
    {
        StartCoroutine(SFX(ac, position, minMax, volume));
    }
    private IEnumerator SFX(AudioClip ac, Transform t, Vector2 v, float volume)
    {
        GameObject GO = Instantiate(sfxPrefab, t.position, Quaternion.identity);
        AudioSource AS = GO.transform.GetComponent<AudioSource>();
        AS.clip = ac;
        AS.spatialBlend = 1f;
        AS.minDistance = v.x;
        AS.maxDistance = v.y;
        AS.dopplerLevel = 0;
        AS.volume = volume;
        AS.Play();
        yield return new WaitForSeconds(ac.length);
        Destroy(GO);
    }
}
