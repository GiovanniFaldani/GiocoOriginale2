using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AICharacterController : MonoBehaviour
{

    public float moveSpeed;

    private Grid grid;

    private void Start()
    {
        grid = FindAnyObjectByType<Grid>();
    }

    private void FollowPath(List<Node> path)
    {
        if (path[0].walkable) FollowNode(path[0]);
    }

    private void FollowNode(Node node)
    {
        transform.Translate((node.worldPosition-transform.position).normalized * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if(grid.path.Count > 0) FollowPath(grid.path);
    }
}
