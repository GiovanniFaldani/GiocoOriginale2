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
    [SerializeField] Vector3 spawnPosition;

    public void Spawn(Spawnable structureType)
    {
        int cost;
        switch (structureType)
        {
            case Spawnable.Wall:
                cost = wallPreviewPrefab.GetComponent<PlaceablePreview>().structureCost;
                if (GameManager.Instance.GetCurrentMoney() >= cost)
                {
                    Instantiate(wallPreviewPrefab, spawnPosition, Quaternion.identity);
                    GameManager.Instance.AddToMoney(-cost);
                }
                else
                {
                    Debug.Log("Not enough money!");
                }
                break;
            case Spawnable.ArcherTurret:
                cost = archerTurretPreviewPrefab.GetComponent<PlaceablePreview>().structureCost;
                if (GameManager.Instance.GetCurrentMoney() >= cost)
                {
                    GameObject turret = Instantiate(archerTurretPreviewPrefab, spawnPosition, Quaternion.identity);
                    turret.transform.localScale = new Vector3(2, 1.2f, 2);
                    GameManager.Instance.AddToMoney(-cost);
                }
                else
                {
                    Debug.Log("Not enough money!");
                }
                break;
        }
    }

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
            preview.transform.localScale = new Vector3(2, 1.2f, 2);
            GameManager.Instance.AddToMoney(-costs[Spawnable.ArcherTurret]);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
