using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static string returnactivescene()
    {
      return SceneManager.GetActiveScene().name.ToString();
    }
    public static int returnactivesceneint()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public static void callScenebyname(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        
    }

    public static void AppendScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
    }
    public static void DeppendScene(string SceneName)
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }
    public static void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public static void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void Exit()
    {
        Application.Quit();
    }
}
