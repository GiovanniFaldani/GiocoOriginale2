using System.Collections.Generic;
using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    public Dictionary<Spawnable, int> costs = new Dictionary<Spawnable, int>
    {
        { Spawnable.Wall, 5 },
        { Spawnable.ArcherTurret, 20},
        { Spawnable.AreaTurret, 40 },
        { Spawnable.MageTurret, 200 },
        { Spawnable.SpineTrap, 30 },
        { Spawnable.SlowTrap, 70 }
    };

    [SerializeField] GameObject wallPreviewPrefab;
    [SerializeField] GameObject archerTurretPreviewPrefab;
    [SerializeField] GameObject alchemistTurretPreviewPrefab;
    [SerializeField] GameObject mageTurretPreviewPrefab;
    [SerializeField] GameObject slowTrapPreviewPrefab;
    [SerializeField] GameObject spineTrapPreviewPrefab;
    [SerializeField] Vector3 spawnPosition;

    public void SpawnWall()
    {
        //int cost = wallPreviewPrefab.GetComponent<PlaceablePreview>().structureCost;
        if (GameManager.Instance.GetCurrentMoney() >= costs[Spawnable.Wall])
        {
            Instantiate(wallPreviewPrefab, spawnPosition, Quaternion.identity);
            GameManager.Instance.AddToMoney(-costs[Spawnable.Wall]);
        }
        else
        {
            GameManager.Instance.DisplayMessage("Not enough money!", 3);
            Debug.Log("Not enough money!");
        }
    }

    public void SpawnArcherTurret()
    {
        //int cost = archerTurretPreviewPrefab.GetComponent<PlaceablePreview>().structureCost;
        if (GameManager.Instance.GetCurrentMoney() >= costs[Spawnable.ArcherTurret])
        {
            GameObject preview = Instantiate(archerTurretPreviewPrefab, spawnPosition, Quaternion.identity);
            preview.transform.parent = null;
            preview.transform.localScale = new Vector3(2, 2, 2);
            GameManager.Instance.AddToMoney(-costs[Spawnable.ArcherTurret]);
        }
        else
        {
            GameManager.Instance.DisplayMessage("Not enough money!", 3);
            Debug.Log("Not enough money!");
        }
    }

    public void SpawnAlchemistTurret()
    {
        //int cost = archerTurretPreviewPrefab.GetComponent<PlaceablePreview>().structureCost;
        if (GameManager.Instance.GetCurrentMoney() >= costs[Spawnable.AreaTurret])
        {
            GameObject preview = Instantiate(alchemistTurretPreviewPrefab, spawnPosition, Quaternion.identity);
            preview.transform.parent = null;
            preview.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            GameManager.Instance.AddToMoney(-costs[Spawnable.AreaTurret]);
        }
        else
        {
            GameManager.Instance.DisplayMessage("Not enough money!", 3);
            Debug.Log("Not enough money!");
        }
    }

    public void SpawnMageTurret()
    {
        //int cost = archerTurretPreviewPrefab.GetComponent<PlaceablePreview>().structureCost;
        if (GameManager.Instance.GetCurrentMoney() >= costs[Spawnable.MageTurret])
        {
            GameObject preview = Instantiate(mageTurretPreviewPrefab, spawnPosition, Quaternion.identity);
            preview.transform.parent = null;
            preview.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            GameManager.Instance.AddToMoney(-costs[Spawnable.MageTurret]);
        }
        else
        {
            GameManager.Instance.DisplayMessage("Not enough money!", 3);
            Debug.Log("Not enough money!");
        }
    }

    public void SpawnSlowTrap()
    {
        //int cost = archerTurretPreviewPrefab.GetComponent<PlaceablePreview>().structureCost;
        if (GameManager.Instance.GetCurrentMoney() >= costs[Spawnable.SlowTrap])
        {
            GameObject preview = Instantiate(slowTrapPreviewPrefab, spawnPosition, Quaternion.identity);
            preview.transform.parent = null;
            preview.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            GameManager.Instance.AddToMoney(-costs[Spawnable.SlowTrap]);
        }
        else
        {
            GameManager.Instance.DisplayMessage("Not enough money!", 3);
            Debug.Log("Not enough money!");
        }
    }

    public void SpawnSpineTrap()
    {
        //int cost = archerTurretPreviewPrefab.GetComponent<PlaceablePreview>().structureCost;
        if (GameManager.Instance.GetCurrentMoney() >= costs[Spawnable.SpineTrap])
        {
            GameObject preview = Instantiate(spineTrapPreviewPrefab, spawnPosition, Quaternion.identity);
            preview.transform.parent = null;
            preview.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            GameManager.Instance.AddToMoney(-costs[Spawnable.SpineTrap]);
        }
        else
        {
            GameManager.Instance.DisplayMessage("Not enough money!", 3);
            Debug.Log("Not enough money!");
        }
    }
}
