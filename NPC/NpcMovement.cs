using static Unit.UnitAnimationSystem.States;
using System.Collections.Generic;
using System.Linq;
using Grid;
using Unit;
using UnityEngine;

namespace NPC
{
    public class NpcMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 4f;
        [SerializeField] private Transform targetPos;
        
        private UnitAnimationSystem _animationSystem;
        private UnitDirections directions;
        private int currentPathIndex;
        private List<Vector3> pathVectorList;

        private void Awake()
        {
            _animationSystem = GetComponent<UnitAnimationSystem>();
            directions = GetComponent<UnitDirections>();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (pathVectorList != null)
            {
                _animationSystem.SetAnimationState(None);
                Vector3 targetPosition = pathVectorList[currentPathIndex];
                if (Vector3.Distance(transform.position, targetPosition) > 0.05f)
                {
                    var position = transform.position;
                    Vector3 moveDir = (targetPosition - position).normalized;
                    directions.MoveDir = moveDir;
                    // float distanceBefore = Vector3.Distance(position, targetPosition);
                    position += moveDir * (speed * Time.deltaTime);

                    transform.position = position;
                }
                else
                {
                    currentPathIndex++;
                    if (currentPathIndex >= pathVectorList.Count)
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
            pathVectorList = null;
        }

        public void SetTargetPosition()
        {
            currentPathIndex = 0;

            pathVectorList = Pathfinding.Instance.FindPath(transform.position, targetPos.position)?.ToList();

            if (pathVectorList != null && pathVectorList.Count > 1)
            {
                pathVectorList.RemoveAt(0);
            }
        }

        internal void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
    }
}