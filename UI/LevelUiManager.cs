using System;
using TMPro;
using UnityEngine;

public class LevelUiManager : MonoBehaviour
{
    public static LevelUiManager Instance=null;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private TMP_Text levelResultsText;
    [SerializeField] private GameObject levelResultsTextGo;
    private Animator warningTextAnimator;

    private void Awake()
    {
        Instance = this;
        warningTextAnimator = warningText.GetComponent<Animator>();
    }

    internal void ShowWarningText(string text)
    {
        warningTextAnimator.Play("WarningText");
        warningText.text = text;
    }
    internal void ShowLevelResultsText(string text,Color color=default)
    {
       // warningTextAnimator.Play("LevelResultsText");
        if (levelResultsTextGo.activeInHierarchy) return;
        levelResultsTextGo.SetActive(true);
        levelResultsText.text = text;
        levelResultsText.color = color;
    }

    public void PlayAudioSource(AudioSource audioSource)
    {
        audioSource.Play();
    }
}