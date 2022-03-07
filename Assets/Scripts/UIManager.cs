using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject creditsScreen;
    private bool creditsActive;
    public GameObject fadeScreen;

    private void Awake()
    {
        instance = this;
        if (creditsScreen != null)
        {
            creditsScreen.SetActive(false);
        }
        if (fadeScreen != null)
        {
            fadeScreen.SetActive(true);
        }
        creditsActive = false;
    }
    private void Start()
    {
        if (fadeScreen != null)
        {
            StartCoroutine(FadeIn());
        }
    }
    private void Update()
    {
        if (creditsActive && Input.GetKeyDown(KeyCode.Escape))
        {
            creditsScreen.SetActive(false);
        }
    }
    public void Play(string scene)
    {
        StartCoroutine(FadeOut("Prototype"));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ToggleCredits()
    {
        Debug.Log("Toggle");
        if (creditsScreen != null)
        {
            creditsActive = !creditsActive;
            creditsScreen.SetActive(creditsActive);
        }
    }
    public IEnumerator FadeOut(string sceneName)
    {
        fadeScreen.SetActive(true);
        Animation anim = fadeScreen.GetComponent<Animation>();
        AnimationClip clip = anim.GetClip("FadeOut");
        anim.clip = clip;
        anim.Play();
        yield return new WaitForSeconds(clip.length);
        SceneNavigator.instance.LoadScene(sceneName);
    }
    public IEnumerator FadeIn()
    {
        fadeScreen.SetActive(true);
        Animation anim = fadeScreen.GetComponent<Animation>();
        AnimationClip clip = anim.GetClip("FadeIn");
        anim.clip = clip;
        anim.Play();
        yield return new WaitForSeconds(clip.length);
        fadeScreen.SetActive(false);
    }

}
