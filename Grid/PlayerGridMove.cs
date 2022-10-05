using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeMonkey.Utils;
using UnityEngine;

namespace Grid
{
    public class PlayerGridMove : MonoBehaviour
    {
        [SerializeField]private float speed = 4f;
        [SerializeField] private Camera cam;

        private UnitDirections directions;
        private int currentPathIndex;
        private List<Vector3> pathVectorList;

        private void Awake()
        {
            directions = GetComponent<UnitDirections>();
        }

        private void Update() {
            HandleMovement();

            if (Input.GetMouseButtonDown(0)) {
                SetTargetPosition(cam.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    
        private void HandleMovement()
        {
            if (pathVectorList != null)
            {
                Vector3 targetPosition = pathVectorList[currentPathIndex];
                if (Vector3.Distance(transform.position, targetPosition) > 0.05f)
                {
                    var position = transform.position;
                    Vector3 moveDir = (targetPosition - position).normalized;
                    directions.MoveDir = moveDir;
                    float distanceBefore = Vector3.Distance(position, targetPosition);
                    position += moveDir * (speed * Time.deltaTime);

                    transform.position = position ;
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
                //idle
            }
        }

        private void StopMoving() {
            pathVectorList = null;
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public void SetTargetPosition(Vector3 targetPosition) {
            currentPathIndex = 0;
            pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition)?.ToList();
            if (pathVectorList != null && pathVectorList.Count > 1)
            {

                pathVectorList.RemoveAt(0);
            }
        }
    }
}
