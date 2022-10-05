using Unity.Mathematics;
using UnityEngine;

namespace Grid
{
    public class GridVisualizer : MonoBehaviour
    {
        [SerializeField] private GameObject gridCell;
        [SerializeField] private Transform parent;

        private void Start()
        {
            for (int x = 0; x < 141; x++) {
                for (int y = 0; y < 80; y++)
                {
                   var cell= Instantiate(gridCell, GetWorldPosition(x, y)+new Vector3(1,1)*.5f, quaternion.identity);
                   cell.transform.parent = parent;
                }
            }
        }

        private Vector3 GetWorldPosition(int x, int y) {
            return new Vector3(x, y) * 1 + Vector3.zero;
        }
    }
}
