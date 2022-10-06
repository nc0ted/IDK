using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid
{
    public class BuildOnGrid : MonoBehaviour
    {
        [SerializeField] private GameObject commonWall;
        [SerializeField] private Camera cam;
        [SerializeField] Sprite rightCornerWall, leftCornerWall, wallDownSprite;
        [SerializeField] private bool commonBuild = true;
        
        private Sprite commonSprite;
        private Sprite currentSprite;
        private int gridX, gridY;
        private GameObject currentPrefab;
        private bool _isWalkable;

        private void Awake()
        {
            currentPrefab = commonWall;
            if (!commonBuild)
            {
                commonSprite = commonWall.GetComponent<SpriteRenderer>().sprite;
                currentSprite = commonSprite;
            }
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                Pathfinding.Instance.GetGrid().GetXY(mouseWorldPosition, out gridX, out gridY);
                Pathfinding.Instance.GetNode(gridX, gridY).SetIsWalkable(_isWalkable);
                Build();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                Pathfinding.Instance.GetGrid().GetXY(mouseWorldPosition, out gridX, out gridY);
                var node = Pathfinding.Instance.GetNode(gridX, gridY);
                if(node.GameObject!=null) 
                    node.GameObject.SetActive(false);
                node.HasWall = false;
                node.SetIsWalkable(true);
            }
        }

        private void Build()
        {
            if (Pathfinding.Instance.GetNode(gridX, gridY).HasWall) return;
            var position = Pathfinding.Instance.GetGrid().GetWorldPosition(gridX, gridY) + new Vector3(1, 1) * 0.5f;
            var wall = Instantiate(currentPrefab, position, quaternion.identity);
            Pathfinding.Instance.GetNode(gridX, gridY).SetHasWall(!_isWalkable);
            Pathfinding.Instance.GetNode(gridX, gridY).GameObject = wall;
            
            if (commonBuild) return;
            if (Pathfinding.Instance.GetNode(gridX, gridY + 1).HasWall &&
                Pathfinding.Instance.GetNode(gridX - 1, gridY + 1).HasWall)
            {
                currentSprite = wallDownSprite;
                Pathfinding.Instance.GetNode(gridX, gridY + 1).SetSprite(rightCornerWall);
            }

            if (Pathfinding.Instance.GetNode(gridX, gridY + 1).HasWall &&
                Pathfinding.Instance.GetNode(gridX + 1, gridY + 1).HasWall)
            {
                currentSprite = wallDownSprite;
                Pathfinding.Instance.GetNode(gridX, gridY + 1).SetSprite(leftCornerWall);
                wall.transform.localRotation = new Quaternion(0, 0, -180, 0);
            }

            if (!Pathfinding.Instance.GetNode(gridX, gridY + 1).HasWall)
            {
                currentSprite = commonSprite;
            }

            wall.GetComponent<SpriteRenderer>().sprite = currentSprite;
        }

        internal void SetPrefab(GameObject prefab, bool isWalkable)
        {
            currentPrefab = prefab;
            _isWalkable = isWalkable;
        }
    }
}