using System;
using NPC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public static Target Instance = null;
    private int _npcCount;
    private int gettedNpcs;

    private void Awake()
    {
        Instance = this;
        _npcCount = FindObjectsOfType<NpcMovement>().Length;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<NpcMovement>()) return;
        gettedNpcs++;
        CheckForLose();
    }

    internal void CheckForLose()
    {
        if (gettedNpcs >= _npcCount)
        {
            GameUiManager.Instance.ShowLevelResultsText("You Lose, target captured",Color.red);
            Invoke(nameof(LoadLevelAgain),5f);
        }
    }
    internal void DecrementNpcCount()
    {
        _npcCount--;
    }
    private void LoadLevelAgain()
    {
        SceneManager.LoadScene(0);
    }
}