using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private int nextSceneId;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadScene(int sceneId,float delay=0)
    {
        nextSceneId = sceneId;
        Invoke(nameof(LoadSceneInstantly),delay);
    }

    private void LoadSceneInstantly()
    {
        SceneManager.LoadScene(nextSceneId);
    }

    public void LoadSceneForBtns(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}