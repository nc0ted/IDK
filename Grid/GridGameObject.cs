/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;

namespace Grid
{
    public class GridGameObject : MonoBehaviour
    {
        [SerializeField] private int width, height;
        [SerializeField] private float cellSize;
        [SerializeField] private Vector3 originPos;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float CellSize { get; set; }
        public Vector3 OriginPos { get; set; }

        private Pathfinding _pathfinding;

        private void Awake() {
            _pathfinding = new Pathfinding(width, height,cellSize,originPos);
            Width = width;
            Height = height;
            OriginPos = originPos;
            CellSize = cellSize;
        }
    }
}
