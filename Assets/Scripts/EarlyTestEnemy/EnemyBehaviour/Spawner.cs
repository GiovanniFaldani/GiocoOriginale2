using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int enemyCount = 10; // Numero massimo di nemici da spawnare
    [SerializeField] private float delayBtwSpawns; // Ritardo tra ogni spawn
    [SerializeField] private ObjectPooler _pooler; // Pooler da cui prendere i nemici
    [SerializeField] private Transform spawnPosition; // Punto nello spazio dove spawnare i nemici

    private float _spawnTimer;
    private int _enemiesSpawned;

    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                Invoke("SpawnEnemy", 0.5f);
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();

        // Usa la posizione assegnata, o la posizione del GameObject stesso come fallback
        Vector3 spawnPos = spawnPosition != null ? spawnPosition.position : transform.position;
        newInstance.transform.position = spawnPos;

        newInstance.SetActive(true);
    }
}
