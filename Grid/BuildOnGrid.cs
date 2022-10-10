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
        private ItemTypes _currentType;
        private Sprite _commonSprite;
        private Sprite _currentSprite;
        private int _gridX, _gridY;
        private GameObject _currentPrefab;
        private bool _isWalkable;

        private void Awake()
        {
            _itemsTexts = FindObjectsOfType<InventoryItemText>();
            _currentPrefab = commonWall;
            _currentType = ItemTypes.Wall;
            if (!commonBuild)
            {
                _commonSprite = commonWall.GetComponent<SpriteRenderer>().sprite;
                _currentSprite = _commonSprite;
            }
        }
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (InventoryInfo.GetItemCount(_currentType) <= 0) return;
                InventoryInfo.DecrementCount(_currentType);
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
                if (node.GameObject != null&&node.GameObject.activeInHierarchy&&node.GameObject.GetComponent<InventoryItem>().Type==_currentType)
                {
                    InventoryInfo.IncrementCount(_currentType);
                    foreach (var item in _itemsTexts)
                    {
                        item.UpdateText();
                    }
                    node.GameObject.SetActive(false);
                }
                else
                {
                    print(_currentType);
                    print(node.GameObject);
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
            wall.GetComponent<AudioSource>().Play();
          
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
            // wall.GetComponent<SpriteRenderer>().sprite = _currentSprite;
        }

        internal void SetPrefab(GameObject prefab, bool isWalkable)
        {
            _currentPrefab = prefab;
            _currentType = _currentPrefab.GetComponent<InventoryItem>().Type;
            _isWalkable = isWalkable;
        }
    }
}