using UnityEngine;


// Handles Destruction of structures
public class DestroyHandler : MonoBehaviour
{
    private StructureSpawner spawner;
    private ReachabilityTest rt;
    private PlacementGrid grid;

    private void Start()
    {
        grid = FindAnyObjectByType<PlacementGrid>().GetComponent<PlacementGrid>();
        spawner = FindAnyObjectByType<StructureSpawner>().GetComponent<StructureSpawner>();
        rt = FindAnyObjectByType<ReachabilityTest>().GetComponent<ReachabilityTest>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1)) DestroyWithRayCast();
    }

    private void DestroyWithRayCast()
    {
        //Raycast mouse position X and Z
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Turret"))
            {
                grid.GetGridSnap(hit.collider.transform.position).built = false;
                StructureType strType = hit.collider.GetComponent<StructureType>();
                GameManager.Instance.AddToMoney(Mathf.RoundToInt(spawner.costs[strType.type] * 0.2f));
                Destroy(hit.collider.gameObject);
                rt.navMesh.BuildNavMesh();
            }
        }
    }
}
