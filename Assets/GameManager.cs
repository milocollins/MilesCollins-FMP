using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    void Start()
    {
        isPaused = false;
        SetMouse(isPaused);
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
        SetMouse(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    private void SetMouse(bool b)
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
}
