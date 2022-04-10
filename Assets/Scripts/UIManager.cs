using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject creditsScreen;
    private bool creditsActive;
    public GameObject fadeScreen;
    public GameObject healthBar;
    public GameObject staminaBar;
    public GameObject deathScreen;
    public GameObject pauseScreen;
    public GameObject winScreen;
    public GameObject settings;
    public GameObject controls;
    public GameObject musicTestObj;
    private bool pauseActive;

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
        if (healthBar != null)
        {
            healthBar.SetActive(false);
        }
        if (staminaBar != null)
        {
            staminaBar.SetActive(false);
        }
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);
        }
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
        creditsActive = false;
        Time.timeScale = 1f;
    }
    private void Start()
    {
        Debug.Log("HIT");
        if (fadeScreen != null)
        {
            StartCoroutine(FadeIn());
        }
        if (SceneNavigator.instance.GetCurrentScene() != "MainMenu")
        {
            ToggleControls();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (creditsActive)
            {
                creditsScreen.SetActive(false);
            }
        }
    }
    public bool CanPause()
    {
        if (pauseScreen == null)
        {
            return false;
        }
        if (winScreen != null)
        {
            if (winScreen.activeInHierarchy)
            {
                return false;
            }
        }
        if (deathScreen != null)
        {
            if (deathScreen.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
    public void TogglePause(bool b)
    {
        pauseScreen.SetActive(b);
        if (b)
        {
            //pauseScreen.transform.Find("Controls").GetComponent<Button>().Select();
            ToggleControls();
            Transform t = pauseScreen.transform.Find("Settings Panel");
            t.Find("Master Volume").GetComponent<Slider>().value = AudioManager.instance.GetVolume("MasterVolume");
            t.Find("Music Volume").GetComponent<Slider>().value = AudioManager.instance.GetVolume("MusicVolume");
            t.Find("UI Volume").GetComponent<Slider>().value = AudioManager.instance.GetVolume("UIVolume");
            t.Find("SFX Volume").GetComponent<Slider>().value = AudioManager.instance.GetVolume("SFXVolume");
            t.Find("Horizontal Sensitivity").GetComponent<Slider>().value = Camera.main.GetComponent<ThirdPersonOrbitCam>().horizontalAimingSpeed;
            t.Find("Vertical Sensitivity").GetComponent<Slider>().value = Camera.main.GetComponent<ThirdPersonOrbitCam>().verticalAimingSpeed;
        }
    }
    public void Play(string scene)
    {
        UIClick();
        StartCoroutine(FadeOut("Prototype"));
    }
    public void QuitGame()
    {
        UIClick();
        Application.Quit();
    }
    public void ToggleCredits()
    {
        Debug.Log("Toggle");
        UIClick();
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
        Debug.Log("Fading");
        fadeScreen.SetActive(true);
        Animation anim = fadeScreen.GetComponent<Animation>();
        AnimationClip clip = anim.GetClip("FadeIn");
        anim.clip = clip;
        anim.Play();
        yield return new WaitForSeconds(clip.length);
        fadeScreen.SetActive(false);
    }
    public void ToggleHealthBar(bool b)
    {
        healthBar.SetActive(b);
    }
    public void UpdateHealth(float health)
    {
        healthBar.GetComponent<Slider>().value = health;
    }
    public void ToggleStaminaBar(bool b)
    {
        staminaBar.SetActive(b);
    }
    public void UpdateStamina(float stamina)
    {
        staminaBar.GetComponent<Slider>().value = stamina;
    }
    public void DeathScreen()
    {
        StartCoroutine(FadeIn());
        deathScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void WinScreen()
    {
        StartCoroutine(FadeIn());
        winScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SFXManager.instance.LoopMusic(false, false, true);
    }
    public void MainMenu()
    {
        UIClick();
        SceneNavigator.instance.LoadScene("MainMenu");
    }
    public void Retry()
    {
        UIClick();
        SceneNavigator.instance.LoadScene(SceneNavigator.instance.GetCurrentScene());
    }
    public void ToggleSettings()
    {
        UIClick();
        settings.SetActive(true);
        pauseScreen.transform.Find("Settings").GetComponent<Image>().color = Color.red;
        controls.SetActive(false);
        pauseScreen.transform.Find("Controls").GetComponent<Image>().color = Color.white;
    }
    public void ToggleControls()
    {
        UIClick();
        controls.SetActive(true);
        pauseScreen.transform.Find("Controls").GetComponent<Image>().color = Color.red;
        settings.SetActive(false);
        pauseScreen.transform.Find("Settings").GetComponent<Image>().color = Color.white;
    }
    public void UpdateMusicVolume(float f)
    {
        AudioManager.instance.SetVolume(f, "MusicVolume");
        SFXManager.instance.TestAMG(AudioManager.instance.Music);
    }
    public void UpdateMasterVolume(float f)
    {
        AudioManager.instance.SetVolume(f, "MasterVolume");
        SFXManager.instance.TestAMG(AudioManager.instance.Master);
    }
    public void UpdateUIVolume(float f)
    {
        AudioManager.instance.SetVolume(f, "UIVolume");
        SFXManager.instance.TestAMG(AudioManager.instance.UI);
    }
    public void UpdateSFXVolume(float f)
    {
        AudioManager.instance.SetVolume(f,"SFXVolume");
        SFXManager.instance.TestAMG(AudioManager.instance.SFX);
    }
    public void SetHorizontalSensitivity(float f)
    {
        UIClick();
        Camera.main.transform.GetComponent<ThirdPersonOrbitCam>().horizontalAimingSpeed = f;
    }
    public void SetVerticalSensitivity(float f)
    {
        UIClick();
        Camera.main.transform.GetComponent<ThirdPersonOrbitCam>().verticalAimingSpeed = f;
    }
    public void UIClick()
    {
        SFXManager.instance.PlayOneShot(SFXManager.instance.clickSFX, AudioManager.instance.UI);
    }
}