using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    internal bool isPaused;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        isPaused = false;
        MouseToggle(isPaused);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseToggle();
        }
    }

    private void PauseToggle()
    {
        isPaused = !isPaused;
        MouseToggle(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    private void MouseToggle(bool b)
    {
        Cursor.visible = b;
        if (b)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void ChangeScene(string sceneName)
    {
        //Fade Out
        SceneNavigator.instance.LoadScene(sceneName);
    }
}
