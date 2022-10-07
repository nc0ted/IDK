using System.Timers;
using NPC;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WavesManager : MonoBehaviour
{
    public static WavesManager Instance=null;
    [SerializeField] private float firstTimerDelay;
    [SerializeField] private float timeToKillEveryone;
    private int _npcCount;
    private NpcMovement[] npcs;
    private int _deathNpcs;
    private bool _won;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
         npcs = FindObjectsOfType<NpcMovement>();
        _npcCount = npcs.Length;
        Invoke(nameof(StartWave),firstTimerDelay);
    }
    private void StartWave()
    {
        foreach (var npc in npcs)
        {
            npc.SetTargetPosition();
        }
        Invoke(nameof(CheckForLoseByTime),timeToKillEveryone);
    }
    private void CheckForLoseByTime()
    {
        if (_won) return;
        GameUiManager.Instance.ShowLevelResultsText("You Lose, time is up",Color.red);
        Invoke(nameof(LoadSceneAgain),5f);
    }
    internal void CheckForWin()
    {
        _deathNpcs++;
        if (_deathNpcs < _npcCount) return;
        _won = true;
        GameUiManager.Instance.ShowLevelResultsText("You Win!",Color.white);
        Invoke(nameof(LoadSceneAgain),5f);
    }

    private void LoadSceneAgain()
    {
        SceneManager.LoadScene(0);
    }
}