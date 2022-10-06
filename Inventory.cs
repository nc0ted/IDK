using Grid;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private BuildOnGrid buildOnGrid;
    [Header("Prefabs")] 
    [SerializeField] private GameObject commonTrap; 
    [SerializeField] private GameObject wall;
    
    public void PickItem(string item)
    {
        switch (item)
        {
            case "Wall":
                buildOnGrid.SetPrefab(wall,false);
                break;
            case "CommonTrap":
                buildOnGrid.SetPrefab(commonTrap,true);
                break;
            default:
                buildOnGrid.SetPrefab(wall,false);
                break;
        }
    }

}