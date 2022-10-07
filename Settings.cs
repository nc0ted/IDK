using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseOrOpenMenu();
        }
    }
    private void CloseOrOpenMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
