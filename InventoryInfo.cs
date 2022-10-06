
using UnityEngine;

public static class InventoryInfo
{
    private static int wallsCount;
    private static int commonTrapsCount;

    public static void SetInfo(int wallsCount,int commonTrapsCount)
    {
        InventoryInfo.wallsCount = wallsCount;
        InventoryInfo.commonTrapsCount = commonTrapsCount;
    }

    public static int GetItemCount(ItemTypes itemType)
    {
        int count=0;
        switch (itemType)
        {
            case ItemTypes.Wall:
                count = wallsCount;
                break;
            case ItemTypes.CommonTrap:
                count = commonTrapsCount;
                break;
        }
        return count;
    }
    public static void DecrementCount(ItemTypes itemType)
    {
        switch (itemType)
        {
            case ItemTypes.Wall:
                wallsCount--;
                break;
            case ItemTypes.CommonTrap:
                commonTrapsCount--;
                break;
        }
    }
    public static void IncrementCount(ItemTypes itemType)
    {
        switch (itemType)
        {
            case ItemTypes.Wall:
                wallsCount++;
                break;
            case ItemTypes.CommonTrap:
                commonTrapsCount++;
                break;
        }
    }
}