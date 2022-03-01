using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{
    public static SceneNavigator instance;

    [SerializeField] private GameObject loadCanvas;
    [SerializeField] private Slider loadBar;

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
    private void Update()
    {
        
    }
    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        loadCanvas.SetActive(true);
        do
        {
            await Task.Delay(100);
            loadBar.value = scene.progress;
        } while (scene.progress < 0.9f);
        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        loadCanvas.SetActive(false);

    }
}
