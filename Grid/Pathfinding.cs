/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using UnityEngine;

namespace Grid
{
    [BurstCompile]
    public class Pathfinding
    {
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        public static Pathfinding Instance { get; private set; }

        private Grid<PathNode> grid;
        private HashSet<PathNode> openList;
        private HashSet<PathNode> closedList;
        
        public Pathfinding(int width, int height,float cellSize,Vector3 originPos)
        {
            Instance = this;
            grid = new Grid<PathNode>(width, height, cellSize, originPos,
                (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
        }

        public Grid<PathNode> GetGrid()
        {
            return grid;
        }

        public HashSet<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
        {
            grid.GetXY(startWorldPosition, out int startX, out int startY);
            grid.GetXY(endWorldPosition, out int endX, out int endY);
            if (!grid.GetGridObject(endX, endY).IsWalkable) return null;
            
            HashSet<PathNode> path = FindPath(startX, startY, endX, endY);
            if (path == null)
            {
                return null;
            }
            HashSet<Vector3> vectorPath = new HashSet<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() +
                               Vector3.one * (grid.GetCellSize() * .5f));
            }

            return vectorPath;
        }

        public HashSet<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            PathNode startNode = grid.GetGridObject(startX, startY);
            PathNode endNode = grid.GetGridObject(endX, endY);

            if (startNode == null || endNode == null)
            {
                // Invalid Path
                return null;
            }

            openList = new HashSet<PathNode> { startNode };
            closedList = new HashSet<PathNode>();

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    PathNode pathNode = grid.GetGridObject(x, y);
                    pathNode.GCost = 99999999;
                    pathNode.CalculateFCost();
                    pathNode.CameFromNode = null;
                }
            }

            startNode.GCost = 0;
            startNode.HCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            while (openList.Count > 0)
            {
                PathNode currentNode = GetLowestFCostNode(openList);
                if (currentNode == endNode)
                {
                    // Reached final node
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
                {
                    if(openList.Count+closedList.Count>5000)
                    {
                        return null;
                    }
                    if (closedList.Contains(neighbourNode)) continue;
                    if (!neighbourNode.IsWalkable)
                    {
                        if(openList.Count+closedList.Count>1700)
                        {
                            return null;
                        }
                        closedList.Add(neighbourNode);
                        continue;
                    }

                    int tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.GCost)
                    {
                        neighbourNode.CameFromNode = currentNode;
                        neighbourNode.GCost = tentativeGCost;
                        neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!openList.Contains(neighbourNode))
                        {
                            if(openList.Count+closedList.Count>9000)
                            {
                                return null;
                            }
                            openList.Add(neighbourNode);
                        }
                    }
                }
            }
            // Out of nodes on the openList
            return null;
        }

        private HashSet<PathNode> GetNeighbourList(PathNode currentNode)
        {
            HashSet<PathNode> neighbourList = new HashSet<PathNode>();

            if (currentNode.x - 1 >= 0)
            {
                // Left
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
                // Left Down
                if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
                // Left Up
                if (currentNode.y + 1 < grid.GetHeight())
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
            }

            if (currentNode.x + 1 < grid.GetWidth())
            {
                // Right
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
                // Right Down
                if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
                // Right Up
                if (currentNode.y + 1 < grid.GetHeight())
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
            }

            // Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
            // Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

            return neighbourList;
        }

        public PathNode GetNode(int x, int y)
        {
            return grid.GetGridObject(x, y);
        }

        private static HashSet<PathNode> CalculatePath(PathNode endNode)
        {
            var path = new HashSet<PathNode> { endNode };
            PathNode currentNode = endNode;
            while (currentNode.CameFromNode != null)
            {
                if(!path.Contains(currentNode.CameFromNode))
                    path.Add(currentNode.CameFromNode); 
                currentNode = currentNode.CameFromNode;
            }
            return path.Reverse().ToHashSet();
        }

        private int CalculateDistanceCost(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }

        private PathNode GetLowestFCostNode(HashSet<PathNode> pathNodeList)
        {
            PathNode lowestFCostNode = pathNodeList.First();
            foreach (var node in pathNodeList)
            {
                if (node.FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = node;
                }
            }
            
            return lowestFCostNode;
        }
    }
}
