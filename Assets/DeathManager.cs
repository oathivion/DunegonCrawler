using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject manager;
    [SerializeField] List<string> menuSceneNames;
    [SerializeField] string deathSceneName;
    public void CleanUp()
    {
        SceneManager.LoadScene(deathSceneName);


    }
    public void Update()
    {
        if (menuSceneNames.Contains(SceneManager.GetActiveScene().name))
        {
            Destroy(canvas);
            Destroy(manager);
            Destroy(gameObject);
        }
    }
}
