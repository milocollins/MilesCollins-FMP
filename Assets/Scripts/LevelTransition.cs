using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    private bool triggered;
    public string targetSceneName;
    private void Awake()
    {
        triggered = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.tag == "Player")
        {
            triggered = true;
            SceneNavigator.instance.LoadScene(targetSceneName);
        }
    }
}
