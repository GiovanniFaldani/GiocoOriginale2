using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// elenco di nodi da dividere in due

// Set di nodi OPEN (da valutare)
// Set di nodi CLOSED (gia' valutati)

// Aggiungiamo il nodo iniziale OPEN

// loop
// currentNode = nodo in OPEN con f-cost minore
// rimuovere currentNode da OPEN
// aggiungere currentNode in CLOSED

// if currentNode = targetNode -> percorso trovato

// foreach neighbor di currentNode
// if neighbor presenta un ostacolo || e' in CLOSED
// skippiamo al next neighbor

// if nuovo percorso fino a neighbor e' piu' corto || neighbor non e' in OPEN
// set f-cost di neighbor 
// set "parent" di neighbor = currentNode (riscostruzione percorso)
// if neighbor non e' OPEN
// Aggiungo neighbor a OPEN

public class Pathfinding : MonoBehaviour
{
    private PlacementGrid grid;

    private void Start()
    {
        grid = FindAnyObjectByType<PlacementGrid>();
    }

    public List<GridSquare> FindPath(Vector3 startPosition, Vector3 endPosition)
    {
        GridSquare startSquare = grid.GetGridSnap(startPosition);
        GridSquare targetSquare = grid.GetGridSnap(endPosition);

        List<GridSquare> openSet = new List<GridSquare>();
        HashSet<GridSquare> closedSet = new HashSet<GridSquare>();

        openSet.Add(startSquare);

        while (openSet.Count > 0)
        {
            GridSquare currentSquare = openSet[0];

            for(int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentSquare.fCost || (
                    openSet[i].fCost == currentSquare.fCost && openSet[i].hCost < currentSquare.hCost))
                {
                    currentSquare = openSet[i];
                }
            }

            openSet.Remove(currentSquare);
            closedSet.Add(currentSquare);

            if (currentSquare == targetSquare)
            { return RetracePath(startSquare, targetSquare);}

            foreach(GridSquare neighbour in grid.GetNeighbours(currentSquare))
            {
                if(neighbour.built || closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentSquare.gCost + GetDistance(currentSquare, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetSquare);

                    neighbour.parent = currentSquare;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        Debug.Log("Test");
        return new List<GridSquare>();
    }

    int GetDistance(GridSquare A, GridSquare B)
    {
        int dstX = Mathf.Abs(A.gridX - B.gridX);
        int dstY = Mathf.Abs(A.gridY - B.gridY);

        if(dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }

        return 14 * dstX + 10 * (dstY - dstX);
    }

    private List<GridSquare> RetracePath(GridSquare startNode, GridSquare endNode)
    {
        List<GridSquare> path = new List<GridSquare>();

        GridSquare currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        // Debug
        grid.path = path;

        return path;
    }
}
