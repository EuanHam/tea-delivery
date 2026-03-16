using System.Collections.Generic;
using UnityEngine;

public static class AStarPathfinder
{
    public static List<IntersectionNode> FindPath(IntersectionNode startNode, IntersectionNode goalNode)
    {
        if (startNode == null || goalNode == null)
        {
            return null;
        }

        List<IntersectionNode> openSet = new List<IntersectionNode>();
        HashSet<IntersectionNode> closedSet = new HashSet<IntersectionNode>();

        Dictionary<IntersectionNode, IntersectionNode> cameFrom = new Dictionary<IntersectionNode, IntersectionNode>();
        Dictionary<IntersectionNode, float> gScore = new Dictionary<IntersectionNode, float>();
        Dictionary<IntersectionNode, float> fScore = new Dictionary<IntersectionNode, float>();

        openSet.Add(startNode);
        gScore[startNode] = 0f;
        fScore[startNode] = Heuristic(startNode, goalNode);

        while (openSet.Count > 0)
        {
            IntersectionNode current = GetLowestFScore(openSet, fScore);

            if (current == goalNode)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (IntersectionNode neighbor in current.neighbors)
            {
                if (neighbor == null || closedSet.Contains(neighbor))
                {
                    continue;
                }

                float tentativeGScore = GetScore(gScore, current) + Vector3.Distance(current.transform.position, neighbor.transform.position);

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGScore >= GetScore(gScore, neighbor))
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = tentativeGScore + Heuristic(neighbor, goalNode);
            }
        }

        return null;
    }

    private static float Heuristic(IntersectionNode a, IntersectionNode b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }

    private static float GetScore(Dictionary<IntersectionNode, float> scores, IntersectionNode node)
    {
        if (scores.TryGetValue(node, out float value))
            return value;

        return Mathf.Infinity;
    }

    private static IntersectionNode GetLowestFScore(List<IntersectionNode> openSet, Dictionary<IntersectionNode, float> fScore)
    {
        IntersectionNode bestNode = openSet[0];
        float bestScore = GetScore(fScore, bestNode);

        for (int i = 1; i < openSet.Count; i++)
        {
            float score = GetScore(fScore, openSet[i]);
            if (score < bestScore)
            {
                bestScore = score;
                bestNode = openSet[i];
            }
        }

        return bestNode;
    }

    private static List<IntersectionNode> ReconstructPath(Dictionary<IntersectionNode, IntersectionNode> cameFrom, IntersectionNode current)
    {
        List<IntersectionNode> path = new List<IntersectionNode>();
        path.Add(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}