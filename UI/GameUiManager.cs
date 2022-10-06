using System;
using TMPro;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public static GameUiManager Instance=null;
    [SerializeField] private TMP_Text warningText;
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
}