using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Grid : MonoBehaviour
{
    [SerializeField] LayerMask wallMask;
    [SerializeField] Vector2 gridWorldSize;
    [SerializeField] float nodeRadius;

    [SerializeField] Transform player;

    private Node[,] grid;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    public List<Node> path;

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2
            - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                 Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius)
                    + Vector3.forward * (y * nodeDiameter + nodeRadius);

                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, wallMask));

                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // individuo punto sulla griglia in percentuale da 0 a 1
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        // clamp perche'devo essere tra tra 0 e 1
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // recupero gli indici della marice in base alla percentuale
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node currentNode)
    {
        List<Node> neighbours = new List<Node>();

        for(int x= - 1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) 
                    continue;

                int checkX = currentNode.gridX + x;
                int checkY = currentNode.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(grid[checkX, checkY]);
            }
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);

            foreach (Node node in grid)
            {
                Gizmos.color = node.walkable ? Color.white : Color.red;

                if (node == playerNode)
                    Gizmos.color = Color.cyan;

                if(path != null)
                {
                    if (path.Contains(node))
                          Gizmos.color = Color.green;
                }

                Gizmos.DrawCube(node.worldPosition, Vector3.one * nodeRadius);
            }
        }
    }


    // Only when node is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);

            foreach (Node node in grid)
            {
                Gizmos.color = node.walkable ? Color.white : Color.red;

                if (node == playerNode)
                {
                    Gizmos.color = Color.cyan;
                }

                Gizmos.DrawCube(node.worldPosition, Vector3.one * nodeRadius);
            }
        }
    }

}
