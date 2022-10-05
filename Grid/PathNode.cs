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
    public class PathNode {

        private Grid<PathNode> grid;
        public int x;
        public int y;

        public int GCost;
        public int HCost;
        public int FCost;

        public bool IsWalkable;
        public PathNode CameFromNode;
        public bool HasWall;
        public GameObject GameObject;

        public PathNode(Grid<PathNode> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            IsWalkable = true;
        }

        public void CalculateFCost() {
            FCost = GCost + HCost;
        }

        public void SetIsWalkable(bool isWalkable) {
            IsWalkable = isWalkable;
            grid.TriggerGridObjectChanged(x, y);
        }
        public void SetHasWall(bool hasWall) {
            HasWall = hasWall;
        }
        public void SetSprite(Sprite sprite) {
            GameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }

        public override string ToString() {
            return x + "," + y;
        }

    }
}
