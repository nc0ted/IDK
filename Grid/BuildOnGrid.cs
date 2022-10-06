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
        [Tooltip("Use this, another not ready")]
        [SerializeField] private bool commonBuild = true;

        private InventoryItemText[] _itemsTexts;
        private Sprite _commonSprite;
        private Sprite _currentSprite;
        private int _gridX, _gridY;
        private GameObject _currentPrefab;
        private bool _isWalkable;

        private void Awake()
        {
            _itemsTexts = FindObjectsOfType<InventoryItemText>();
            _currentPrefab = commonWall;
            if (!commonBuild)
            {
                _commonSprite = commonWall.GetComponent<SpriteRenderer>().sprite;
                _currentSprite = _commonSprite;
            }
        }
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (InventoryInfo.GetItemCount(_currentPrefab.GetComponent<InventoryItem>().Type) <= 0) return;
            if (Input.GetMouseButtonDown(0))
            {
                InventoryInfo.DecrementCount(_currentPrefab.GetComponent<InventoryItem>().Type);
                foreach (var item in _itemsTexts)
                {
                    item.UpdateText();
                }
                Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                Pathfinding.Instance.GetGrid().GetXY(mouseWorldPosition, out _gridX, out _gridY);
                var node = Pathfinding.Instance?.GetNode(_gridX, _gridY);
                if (node == null) return;
                Pathfinding.Instance?.GetNode(_gridX, _gridY)?.SetIsWalkable(_isWalkable);
                Build();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                Pathfinding.Instance.GetGrid().GetXY(mouseWorldPosition, out _gridX, out _gridY);
                var node = Pathfinding.Instance.GetNode(_gridX, _gridY);
                if (node.GameObject != null)
                {
                    InventoryInfo.IncrementCount(node.GameObject.GetComponent<InventoryItem>().Type);
                    foreach (var item in _itemsTexts)
                    {
                        item.UpdateText();
                    }
                    node.GameObject.SetActive(false);
                }
                node.HasWall = false;
                node.SetIsWalkable(true);
            }
        }

        private void Build()
        {
            if (Pathfinding.Instance.GetNode(_gridX, _gridY).HasWall) return;
            var position = Pathfinding.Instance.GetGrid().GetWorldPosition(_gridX, _gridY) + new Vector3(1, 1) * 0.5f;
            var wall = Instantiate(_currentPrefab, position, quaternion.identity);
            Pathfinding.Instance.GetNode(_gridX, _gridY).SetHasWall(!_isWalkable);
            Pathfinding.Instance.GetNode(_gridX, _gridY).GameObject = wall;
            
            if (commonBuild) return;
            if (Pathfinding.Instance.GetNode(_gridX, _gridY + 1).HasWall &&
                Pathfinding.Instance.GetNode(_gridX - 1, _gridY + 1).HasWall)
            {
                _currentSprite = wallDownSprite;
                Pathfinding.Instance.GetNode(_gridX, _gridY + 1).SetSprite(rightCornerWall);
            }

            if (Pathfinding.Instance.GetNode(_gridX, _gridY + 1).HasWall &&
                Pathfinding.Instance.GetNode(_gridX + 1, _gridY + 1).HasWall)
            {
                _currentSprite = wallDownSprite;
                Pathfinding.Instance.GetNode(_gridX, _gridY + 1).SetSprite(leftCornerWall);
                wall.transform.localRotation = new Quaternion(0, 0, -180, 0);
            }

            if (!Pathfinding.Instance.GetNode(_gridX, _gridY + 1).HasWall)
            {
                _currentSprite = _commonSprite;
            }
            wall.GetComponent<SpriteRenderer>().sprite = _currentSprite;
        }

        internal void SetPrefab(GameObject prefab, bool isWalkable)
        {
            _currentPrefab = prefab;
            _isWalkable = isWalkable;
        }
    }
}