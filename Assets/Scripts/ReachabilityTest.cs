using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ReachabilityTest : MonoBehaviour
{
    [SerializeField] private NavMeshAgent spawnPosition;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private NavMeshSurface navMesh;

    [HideInInspector]
    public bool pathAvailable = false;
    public NavMeshPath navMeshPath;

    private void Start()
    {
        navMeshPath = new NavMeshPath();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CalculateNewPathReachable() == true)
            {
                pathAvailable = true;
                Debug.Log("Path available");
            }
            else
            {
                pathAvailable = false;
                Debug.Log("Path not available");
            }
        }
    }

    public bool CalculateNewPathReachable ()
    {
        navMesh.BuildNavMesh();
        spawnPosition.CalculatePath(targetPosition.position, navMeshPath);
        if (navMeshPath.status == NavMeshPathStatus.PathComplete) return true;
        return false;
    }
}

