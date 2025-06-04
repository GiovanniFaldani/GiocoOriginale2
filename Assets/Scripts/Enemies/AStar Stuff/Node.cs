using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;

    public int gCost;
    public int hCost;
    public int FCost {  get { return gCost + hCost; } }

    public int gridX;
    public int gridY;

    public Node parent;

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }
}
