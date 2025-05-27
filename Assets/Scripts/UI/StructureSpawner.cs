using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    [SerializeField] GameObject wallPreviewPrefab;
    [SerializeField] Vector3 spawnPosition;

    public void SpawnWall()
    {
        int cost = wallPreviewPrefab.GetComponent<PlaceablePreview>().structureCost;
        if (GameManager.Instance.GetCurrentMoney() > cost)
        {
            Instantiate(wallPreviewPrefab, spawnPosition, Quaternion.identity);
            GameManager.Instance.AddToMoney(-cost);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
