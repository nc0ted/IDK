using static Unit.UnitAnimationSystem.States;
using System.Collections.Generic;
using System.Linq;
using Grid;
using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace NPC
{
    public class NpcMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 4f;
        [SerializeField] private Transform targetPos;

        private UnitAnimationSystem _animationSystem;
        private UnitDirections _directions;
        private int _currentPathIndex;
        private List<Vector3> _pathVectorList;

        private void Awake()
        {
            _animationSystem = GetComponent<UnitAnimationSystem>();
            _directions = GetComponent<UnitDirections>();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_pathVectorList != null)
            {
                Vector3 targetPosition = _pathVectorList[_currentPathIndex];
                if (Vector3.Distance(transform.position, targetPosition) > 0.05f)
                {
                    var position = transform.position;
                    Vector3 moveDir = (targetPosition - position).normalized;
                    _directions.MoveDir = moveDir;
                    // float distanceBefore = Vector3.Distance(position, targetPosition);
                    position += moveDir * (speed * Time.deltaTime);

                    transform.position = position;
                }
                else
                {
                    _currentPathIndex++;
                    if (_currentPathIndex >= _pathVectorList.Count)
                    {
                        StopMoving();
                    }
                }
            }
            else
            {
                _animationSystem.SetAnimationState(Idle);
            }
        }

        private void StopMoving()
        {
            Target.Instance.CheckForLose();
            _pathVectorList = null;
        }

        public void SetTargetPosition()
        {
            _currentPathIndex = 0;

            _pathVectorList = Pathfinding.Instance.FindPath(transform.position, targetPos.position)?.ToList();

            if (_pathVectorList != null && _pathVectorList.Count > 1)
            {
                _pathVectorList.RemoveAt(0);
            }
            if (_pathVectorList == null)
            {
                LevelUiManager.Instance.ShowWarningText("Dont find a path");
            }
            Invoke(nameof(SetTargetPosition),1.5f);
        }

        internal void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
    }
}