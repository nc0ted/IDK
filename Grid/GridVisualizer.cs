using System;
using Unity.Mathematics;
using UnityEngine;

namespace Grid
{
    public class GridVisualizer : MonoBehaviour
    {
        [SerializeField] private GameObject gridCell;
        [SerializeField] private Transform parent;
        private GridGameObject _grid;
        

        private void Start()
        {
            _grid = FindObjectOfType<GridGameObject>();
            for (int x = 0; x <_grid.Width; x++) {
                for (int y = 0; y < _grid.Height; y++)
                {
                   var cell= Instantiate(gridCell, GetWorldPosition(x, y)+new Vector3(1,1)*.5f, quaternion.identity);
                   cell.transform.parent = parent;
                }
            }
        }

        private Vector3 GetWorldPosition(int x, int y) {
            return new Vector3(x, y) * _grid.CellSize + _grid.OriginPos;
        }
    }
}
