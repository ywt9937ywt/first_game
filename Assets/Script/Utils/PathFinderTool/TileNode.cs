using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class TileNode
{
    public TileNode parent;
    public Hex_tile target;
    public Hex_tile destination;
    public Hex_tile origin;

    public int baseCost;
    public int costFromOrigin;
    public int costToDestination;
    public int pathCost;

    public TileNode(Hex_tile current, Hex_tile origin, Hex_tile destination, int pathCost)
    {
        parent = null;
        this.target = current;
        this.origin = origin;
        this.destination = destination;

        baseCost = 1;
        //costFromOrigin = (int)Vector2Int.Distance(current.recCoordinate, origin.recCoordinate);
        //costToDestination = (int)Vector2Int.Distance(current.recCoordinate, destination.recCoordinate);
        //costFromOrigin = Mathf.Abs((current.recCoordinate-origin.recCoordinate).x) + Mathf.Abs((current.recCoordinate - origin.recCoordinate).y);
        costToDestination = Mathf.Abs((current.recCoordinate - destination.recCoordinate).x) + Mathf.Abs((current.recCoordinate - destination.recCoordinate).y);
        this.pathCost = pathCost;
    }

    public int GetCost()
    {
        return pathCost + baseCost+ costToDestination;
    }

    public void SetParent(TileNode node)
    {
        this.parent = node;
    }

    public static List<Hex_tile> FindPath(Hex_tile origin, Hex_tile destination)
    {
        Dictionary<Hex_tile, TileNode> nodesNotEvaluated = new Dictionary<Hex_tile, TileNode>();
        Dictionary<Hex_tile, TileNode> nodesEvaluated = new Dictionary<Hex_tile, TileNode>();

        TileNode startNode = new TileNode(origin, origin, destination, 0);
        nodesNotEvaluated.Add(origin, startNode);

        bool gotPath = EvaluateNextNode(nodesNotEvaluated, nodesEvaluated, origin, destination, out List<Hex_tile> path);

        while (!gotPath)
        {
            gotPath = EvaluateNextNode(nodesNotEvaluated, nodesEvaluated, origin, destination, out path);
        }

        return path;
    }

    private static bool EvaluateNextNode(Dictionary<Hex_tile, TileNode> nodesNotEvaluated, Dictionary<Hex_tile, TileNode> nodesEvaluated,
        Hex_tile origin, Hex_tile destination, out List<Hex_tile> path)
    {
        TileNode currentNode = GetCheapestNode(nodesNotEvaluated.Values.ToArray()); 

        if(currentNode == null)
        {
            path = new List<Hex_tile>();
            return false;
        }

        nodesNotEvaluated.Remove(currentNode.target);
        nodesEvaluated.Add(currentNode.target, currentNode);
        path = new List<Hex_tile>();
        if (currentNode.target == destination)
        {
            path.Add(currentNode.target);
            while(currentNode.target != origin)
            {
                path.Add(currentNode.parent.target);
                currentNode = currentNode.parent;
            }
            return true;
        }
        List<TileNode> neighbours = new List<TileNode>();
        
        foreach (Hex_tile tile in currentNode.target.neighbours)
        {
            TileNode node = new TileNode(tile, origin, destination, currentNode.GetCost());
            if (tile.tileType != TileType.Standard)
            {
                node.baseCost = 999999;
            }
            neighbours.Add(node);
        }

        foreach (TileNode nb in neighbours)
        {
            if (nodesEvaluated.Keys.Contains(nb.target)) continue;
            if (!nodesNotEvaluated.Keys.Contains(nb.target))
            {
                nb.SetParent(currentNode);
                nodesNotEvaluated.Add(nb.target, nb);
            }else {
                if(nb.GetCost() < nodesNotEvaluated[nb.target].GetCost())
                {
                    nodesNotEvaluated[nb.target] = nb;
                }
            }
        }
        return false;
    }

    private static TileNode GetCheapestNode(TileNode[] nodesNotEvaluated)
    {
        if (nodesNotEvaluated.Length == 0) return null;
        TileNode selectedNode = nodesNotEvaluated[0];
        for(int i = 0; i< nodesNotEvaluated.Length; i++)
        {
            TileNode currentNode = nodesNotEvaluated[i];
            if(currentNode.GetCost() < selectedNode.GetCost())
            {
                selectedNode = currentNode;
            }
            else if(currentNode.GetCost() == selectedNode.GetCost() && currentNode.costToDestination < selectedNode.costToDestination)
            {
                selectedNode = currentNode;
            }
        }
        return selectedNode;
    }
}

