using NPC;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static Target Instance = null;
    private int _npcCount;
    private int _gettedNpcs;

    private void Awake()
    {
        Instance = this;
        _npcCount = FindObjectsOfType<NpcMovement>().Length;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<NpcMovement>()) return;
        _gettedNpcs++;
        CheckForLose();
    }

    internal void CheckForLose()
    {
        if (_gettedNpcs < _npcCount) return;
        LevelUiManager.Instance.ShowLevelResultsText("You Lose, target captured",Color.red);
    }
    internal void DecrementNpcCount()
    {
        _npcCount--;
    }
}