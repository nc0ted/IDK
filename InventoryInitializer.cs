using System;
using UnityEngine;

public class InventoryInitializer : MonoBehaviour
{
    [SerializeField] private int wallsCount;
    [SerializeField] private int commonTrapsCount;
    private void Awake()
    {
        InventoryInfo.SetInfo(wallsCount,commonTrapsCount);
    }
}