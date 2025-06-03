using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StructureSelector : MonoBehaviour
{
    [SerializeField] private Color outlineColor;
    private StructureSpawner spawner;
    private ReachabilityTest rt;
    private PlacementGrid grid;

    private GameObject selected = null;

    private void Start()
    {
        grid = FindAnyObjectByType<PlacementGrid>().GetComponent<PlacementGrid>();
        spawner = FindAnyObjectByType<StructureSpawner>().GetComponent<StructureSpawner>();
        rt = FindAnyObjectByType<ReachabilityTest>().GetComponent<ReachabilityTest>();
    }

    private void Update()
    {
        if (selected != null)
        {
            // rotation and deletion
            if (Input.GetKeyDown(KeyCode.Q))
            {
                selected.gameObject.transform.rotation *= Quaternion.Euler(0, -90, 0);
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                selected.gameObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                SelectStructure();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                DeleteSelected();
            }

        }
        else
        {
            // selection
            if(Input.GetMouseButtonDown(0)) SelectStructure();
        }
    }

    private void SelectStructure()
    {
        //Raycast mouse position X and Z
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Turret"))
            {
                // deselect previous selection if it exists
                List<Material> m_list = new List<Material>();
                if (selected != null)
                {
                    Destroy(selected.GetComponent<Outline>());
                }

                // select new object
                // TODO add range FBX display
                selected = hit.collider.gameObject;

                selected.AddComponent<Outline>();
                selected.GetComponent<Outline>().OutlineColor = outlineColor;
            }
            else if(selected != null)
            {
                Destroy(selected.GetComponent<Outline>());
                selected = null;
            }
        }
        else if (selected != null)
        {
            Destroy(selected.GetComponent<Outline>());
            selected = null;
        }
    }

    private void DeleteSelected()
    {
        grid.GetGridSnap(selected.transform.position).built = false;
        StructureType strType = selected.GetComponentInChildren<StructureType>();
        GameManager.Instance.AddToMoney(Mathf.RoundToInt(spawner.costs[strType.type] * 0.2f));
        Destroy(selected);
        rt.navMesh.BuildNavMesh();
        selected = null;
    }
}
