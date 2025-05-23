using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    [SerializeField] GameObject wallPreviewPrefab;
    [SerializeField] Vector3 spawnPosition;

    public void SpawnWall()
    {
        Instantiate(wallPreviewPrefab, spawnPosition, Quaternion.identity);
    }
}
