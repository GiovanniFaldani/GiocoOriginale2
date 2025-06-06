using UnityEngine;

public class GridSquare
{
    public bool built = false;
    public bool fortress = false;
    public int gridX;
    public int gridY;
    public Vector3 worldPosition;

    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }
    public GridSquare parent;

    public GridSquare(Vector3 _worldPosition,  int _gridX, int _gridY)
    {
        this.worldPosition = _worldPosition;
        this.gridX = _gridX;
        this.gridY = _gridY;
    }
}
