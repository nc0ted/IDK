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
        print(item+" ITE<M");
        switch (item)
        {
            case "Wall":
                print("WALL");
                buildOnGrid.SetPrefab(wall,false);
                break;
            case "CommonTrap":
                print("BBBOUNI TRAP");
                buildOnGrid.SetPrefab(commonTrap,true);
                break;
        }
        print(commonTrap.GetComponent<InventoryItem>().Type+" TRAP TYPE");
    }
}