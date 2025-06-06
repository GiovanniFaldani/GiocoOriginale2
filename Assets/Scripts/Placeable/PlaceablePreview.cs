using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlaceablePreview: MonoBehaviour
{
    [SerializeField] private GameObject placePrefab;
    [SerializeField] public int structureCost;
    [SerializeField] private Spawnable structureType;
    private PlacementGrid grid;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private ReachabilityTest rt;

    private bool disableBuild = false;

    private void Start()
    {
        grid = FindAnyObjectByType<PlacementGrid>();
        rt = FindAnyObjectByType<ReachabilityTest>().GetComponent<ReachabilityTest>();
    }

    private void Update()
    {
        // show preview with only mesh, snap to grid
        FollowMouseXZGridSnap();
        if (Input.GetMouseButtonUp(0)) Place();
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
        // Prevent building over something
        if (disableBuild)
        {
            Debug.Log("Invalid placement over object!");
            GameManager.Instance.DisplayMessage("Invalid placement over object!", 3);
            Destroy(this.gameObject);
            GameManager.Instance.AddToMoney(structureCost); // refund Player
            return;
        }
        // instantiate prefab with mesh off
        GameObject temp = Instantiate(placePrefab, transform.position, transform.rotation);

        // rescale adjustments, set grid built attribute. Do not set built for traps
        switch (structureType)
        {
            case Spawnable.Wall:
                grid.GetGridSnap(transform.position).built = true;
                break;
            case Spawnable.ArcherTurret:
                grid.GetGridSnap(transform.position).built = true;
                temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + 2, temp.transform.position.z);
                temp.transform.localScale = new Vector3(2, 2, 2);
                break;
            case Spawnable.AreaTurret:
                grid.GetGridSnap(transform.position).built = true;
                temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + 2, temp.transform.position.z);
                break;
            case Spawnable.MageTurret:
                grid.GetGridSnap(transform.position).built = true;
                temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + 2, temp.transform.position.z);
                break;
            case Spawnable.SpineTrap:
                grid.GetGridSnap(transform.position).built = false;
                temp.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                break;
            case Spawnable.SlowTrap:
                grid.GetGridSnap(transform.position).built = false;
                temp.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                break;
        }
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
            Debug.Log("Impossible placement, no closed paths allowed!");
            GameManager.Instance.DisplayMessage("Impossible placement, no closed paths allowed!", 3);
            transform.GetChild(0).gameObject.SetActive(true);
            GameManager.Instance.AddToMoney(structureCost); // refund Player
        }
        Destroy(this.gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Fortress")
    //        || collision.collider.CompareTag("Turret"))
    //    {
    //        disableBuild = true;
    //    }

    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Fortress") 
            || collision.collider.CompareTag("Turret") || collision.collider.CompareTag("Trap"))
        {
            disableBuild = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Fortress")
            || collision.collider.CompareTag("Turret") || collision.collider.CompareTag("Trap"))
        {
            disableBuild = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Fortress") 
            || other.CompareTag("Turret") || other.CompareTag("Trap"))
        {
            disableBuild = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Fortress")
            || other.CompareTag("Turret") || other.CompareTag("Trap"))
        {
            disableBuild = false;
        }

    }


}
