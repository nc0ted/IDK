using NPC;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public static WavesManager Instance=null;
    [SerializeField] private TimerVisualizer timer;
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
        timer.StartTimer(firstTimerDelay);
         npcs = FindObjectsOfType<NpcMovement>();
        _npcCount = npcs.Length;
        Invoke(nameof(StartWave),firstTimerDelay);
    }
    private void StartWave()
    {
        timer.StartTimer(timeToKillEveryone);
        foreach (var npc in npcs)
        {
            npc.SetTargetPosition();
        }
        Invoke(nameof(CheckForLoseByTime),timeToKillEveryone);
    }
    private void CheckForLoseByTime()
    {
        if (_won) return;
        LevelUiManager.Instance.ShowLevelResultsText("You Lose, time is up",Color.red);
        GameManager.Instance.LoadScene(1,5f);
    }
    internal void CheckForWin()
    {
        _deathNpcs++;
        if (_deathNpcs < _npcCount) return;
        _won = true;
        LevelUiManager.Instance.ShowLevelResultsText("You Win!",Color.green);
        GameManager.Instance.LoadScene(1,5f);
    }
}