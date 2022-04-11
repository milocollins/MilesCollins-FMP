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
        else if (amg == AudioManager.instance.Music && musicTestObj == null)
        {
            StartCoroutine(OneShotTest(musicTest, amg, 1.5f));
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
    //public void LoopMusic(bool pause, bool dead, bool success)
    //{
    //    GameObject GO = Instantiate(sfxPrefab);
    //    AudioSource AS = GO.transform.GetComponent<AudioSource>();
    //    AS.loop = true;
    //    AS.outputAudioMixerGroup = AudioManager.instance.Music;
    //    string s = SceneNavigator.instance.GetCurrentScene();
    //    if (!pause && !dead && !success)
    //    {
    //        AudioManager.instance.SetMusicLoop(GO);
    //        switch (s)
    //        {
    //            case "MainMenu":
    //                AS.clip = AudioManager.instance.musicList[0];
    //                break;
    //            case "Prototype":
    //                AS.clip = AudioManager.instance.musicList[1];
    //                break;
    //            case "Level 2":
    //                AS.clip = AudioManager.instance.musicList[1];
    //                break;
    //            case "Level 3":
    //                AS.clip = AudioManager.instance.musicList[1];
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    else if (pause)
    //    {
    //        AudioManager.instance.SetPauseMusic(GO, GameManager.instance.isPaused);
    //        AS.clip = AudioManager.instance.musicList[2];
    //    }
    //    else if (dead)
    //    {
    //        AudioManager.instance.SetMusicLoop(GO);
    //        AS.clip = AudioManager.instance.musicList[3];
    //        AudioManager.instance.SetMusicLoop(GO);
    //    }
    //    else if (success)
    //    {
    //        AudioManager.instance.SetMusicLoop(GO);
    //        AS.clip = AudioManager.instance.musicList[4];
    //        AudioManager.instance.SetMusicLoop(GO);
    //    }
    //    AS.Play();
    //}
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
            AS.clip = AudioManager.instance.pauseMusic;
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
        if (gameOver)
        {
            AS.clip = AudioManager.instance.gameOverMusic;
        }
        else if (gameWin)
        {
            AS.clip = AudioManager.instance.gameWinMusic;
        }
        else
        {
            switch (SceneNavigator.instance.GetCurrentScene())
            {
                case "MainMenu":
                    AS.clip = AudioManager.instance.mainMenuMusic;
                    break;
                case "Prototype":
                    AS.clip = AudioManager.instance.gameplayMusic;
                    break;
                case "Level 2":
                    AS.clip = AudioManager.instance.gameplayMusic;
                    break;
                case "Level 3":
                    AS.clip = AudioManager.instance.gameplayMusic;
                    break;
                default:
                    break;
            }
            AS.Play();
        }

    }
}
