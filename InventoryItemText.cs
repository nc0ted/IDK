using System;
using TMPro;
using UnityEngine;

public class InventoryItemText : MonoBehaviour
{
    [SerializeField] private ItemTypes itemType;
    private TMP_Text _text;
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = InventoryInfo.GetItemCount(itemType).ToString();
    }

    internal void UpdateText()
    {
        _text.text = InventoryInfo.GetItemCount(itemType).ToString();
    }
}