using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject creditsScreen;
    private bool creditsActive;
    private void Awake()
    {
        if (creditsScreen != null)
        {
            creditsScreen.SetActive(false);
        }
        creditsActive = false;
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
        SceneNavigator.instance.LoadScene(scene);
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
}
