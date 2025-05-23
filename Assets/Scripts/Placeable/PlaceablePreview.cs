using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlaceablePreview: MonoBehaviour
{
    [SerializeField] private GameObject placePrefab;
    private PlacementGrid grid;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private ReachabilityTest rt;

    private void Start()
    {
        grid = FindAnyObjectByType<PlacementGrid>();
        rt = FindAnyObjectByType<ReachabilityTest>().GetComponent<ReachabilityTest>();
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshFilter.mesh = placePrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        meshRenderer.material = placePrefab.GetComponentInChildren<MeshRenderer>().sharedMaterial;
    }

    private void Update()
    {
        // show preview with only mesh, snap to grid
        FollowMouseXZGridSnap();
        if (Input.GetMouseButtonDown(0)) Place();
    }

    private void FollowMouseXZGridSnap()
    {
        //Raycast mouse position X and Z
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 xzPoint = new Vector3(hit.point.x, 0, hit.point.z); // set height to 0
            transform.position = grid.GetGridSnap(xzPoint).worldPosition;
        }
    }

    private void Place()
    {
        // instantiate prefab with mesh off
        GameObject temp = Instantiate(placePrefab, transform.position, transform.rotation);
        temp.transform.parent = null; // unparent
        temp.GetComponentInChildren<MeshRenderer>().enabled = false;

        // re-bake navmesh
        rt.navMesh.BuildNavMesh();
        
        // check reachability
        if(rt.CalculateNewPathReachable())
        {
            // if reachable, activate mesh
            temp.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(temp);
            rt.navMesh.BuildNavMesh();
            Debug.Log("Impossible placement");
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
