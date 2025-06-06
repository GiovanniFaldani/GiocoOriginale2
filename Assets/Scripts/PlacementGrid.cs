using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlacementGrid : MonoBehaviour
{
    [SerializeField] int gridSizeX;
    [SerializeField] int gridSizeY;
    [SerializeField] float gridHalfStep;

    public GridSquare[,] grid;
    public List<GridSquare> path;

    private static PlacementGrid instance;
    public static PlacementGrid Instance {  get { return instance; } }

    private void Awake()
    {
        instance = this;

        Vector3 center = transform.position;
        grid = new GridSquare[gridSizeX, gridSizeY];

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPosition = new Vector3(
                    transform.position.x + gridHalfStep + x * (2 * gridHalfStep), 
                    transform.position.y, 
                    transform.position.z + gridHalfStep + y * (2 * gridHalfStep));

                grid[x, y] = new GridSquare(worldPosition, x, y);

                if ((x == 9 || x == 10) && (y == 9 || y == 10))
                { grid[x, y].fortress = true; }
            }
        }
    }
    /*
    private void OnDrawGizmosSelected()
    {
        float displayDelta = 0.1f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + gridSizeX * gridHalfStep,
            transform.position.y,
            transform.position.z + gridSizeY * gridHalfStep)
            , new Vector3(gridSizeX * 2 * gridHalfStep, 1, gridSizeY * 2 * gridHalfStep));

        if (grid != null)
        {
            //Vector3 gridCheck = new Vector3(-9, 0, -5);
            //GridSquare check = GetGridSnap(gridCheck);

            foreach (GridSquare gsq in grid)
            {
                Gizmos.color = Color.blue;

                if (path != null)
                {
                    //if(gsq == check)
                    if (path.Contains(gsq))
                    { Gizmos.color = Color.red; }

                }
                if (gsq.fortress)
                { Gizmos.color = Color.yellow; }

                Gizmos.DrawCube(gsq.worldPosition, Vector3.one * (2 * (gridHalfStep - displayDelta)));
            }
        }
    }
    */

    public GridSquare GetGridSnap(Vector3 worldPosition)
    {
        // individuo punto sulla griglia in percentuale da 0 a 1
        Vector3 diff = worldPosition - transform.position;
        //Debug.Log("diff = " + diff.ToString());
        float percentX = (Mathf.Abs(diff.x)) / (gridSizeX * 2 * gridHalfStep);
        float percentY = (Mathf.Abs(diff.z)) / (gridSizeY * 2 * gridHalfStep);

        //Debug.Log("percentX = " + percentX + " percentY = " + percentY);
        // clamp perche'devo essere tra tra 0 e 1
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // recupero gli indici della marice in base alla percentuale
        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
        //Debug.Log("X = " + x + " Y = " + y);

        if (diff.x < 0 && diff.z < 0) return grid[0, 0];
        if (diff.x < 0) return grid[0, y];
        if (diff.z < 0) return grid[x, 0];

        return grid[x, y];
    }
    public List<GridSquare> GetNeighbours(GridSquare currentSquare)
    {
        List<GridSquare> neighbours = new List<GridSquare>();

        Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
        foreach (Vector2 dir in dirs)
        {
            int checkX = currentSquare.gridX + (int)dir.x;
            int checkY = currentSquare.gridY + (int)dir.y;

            if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                neighbours.Add(grid[checkX, checkY]);

        }

        return neighbours;
    }
    public int GetEnemyAmount(GridSquare square)
    {
        Collider[] coll = Physics.OverlapBox(square.worldPosition, gridHalfStep * Vector3.one);
        int counter = 0;
        foreach (Collider col in coll) 
        { 
            if (col.CompareTag("Enemy")) 
            {  counter++; } 
        } 
        return counter;
    }
}
