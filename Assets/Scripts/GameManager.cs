using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject fadeScreen;

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
        if (fadeScreen != null)
        {
            fadeScreen.SetActive(true);
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
        //StartCoroutine(UIManager.instance.FadeToggle());
        //StartCoroutine(WaitForFade(sceneName));
        SceneNavigator.instance.LoadScene(sceneName);
    }
    //private IEnumerator WaitForFade(string sceneName)
    //{
    //    Debug.Log("Waiting");
    //    yield return new WaitForSeconds(4f);
    //    Debug.Log("Done");
    //    SceneNavigator.instance.LoadScene(sceneName);
    //}
    public static void QuitGame()
    {
        Application.Quit();
    }
    public static void DestroyObject(GameObject go)
    {
        Destroy(go);
    }
}
