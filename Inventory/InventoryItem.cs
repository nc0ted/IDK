using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public ItemTypes Type;

    private void Start()
    {
        var grasses = Physics2D.OverlapCircleAll(transform.position,0.7f);
        foreach (var grass in grasses)
        {
            if (grass != null && grass.CompareTag("Grass"))
            {
                grass.gameObject.SetActive(false);
            }
        }
    }
}