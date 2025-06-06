using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] Enemy[] enemyList = new Enemy[4];
    [SerializeField] float spawnDistance = 1.0f;

    public float currentWave = 0;
    public int enemiesAlive = 0;
    [SerializeField] private float waveStrenght = 0;

    [SerializeField] private float waveEndTimer = 7f;
    private float spawnTimer = 0;

    private static WaveHandler instance;
    public static WaveHandler Instance {  get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (enemiesAlive == 0)
        {
            waveEndTimer -= Time.deltaTime;
            if (waveEndTimer < 0 )
            {
                currentWave++;
                waveStrenght = GetWaveValue();
                waveEndTimer = 7f;
            }
        }

        if (waveStrenght > 0)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer < 0 )
            {
                spawnTimer = 0.6f;
                SpawnEnemy();
            }
        }
    }
    public void CreateEnemy(GridSquare square, Vector3 spawnPoint)
    {
        List<Enemy> affordableEnemies = new List<Enemy>();
        foreach (Enemy enemy in enemyList)
        {
            if (enemy.waveWeight <= waveStrenght)
            {
                if (enemy.waveWeight != 4 || currentWave % 4 == 0)
                { affordableEnemies.Add(enemy); }
            }
        }

        Enemy en = Instantiate(affordableEnemies[Random.Range(0,affordableEnemies.Count)]);
        en.currentSquare = square; 
        en.transform.position = spawnPoint - Vector3.down;
        enemiesAlive++;
        waveStrenght -= en.waveWeight;
    }

    public void SpawnEnemy()
    {
        bool spawnCheck = false;
        while (!spawnCheck)
        {
            int dir = Random.Range(0, 4);
            Vector2 gridSpot = Vector2.zero;
            Vector3 spawnDir = Vector3.zero;

            switch (dir)
            {
                case 0:
                    gridSpot = new Vector2(0, Random.Range(0, 20));
                    spawnDir = Vector3.left;
                    break;
                case 1:
                    gridSpot = new Vector2(Random.Range(0, 20), 19);
                    spawnDir = Vector3.forward;
                    break;
                case 2:
                    gridSpot = new Vector2(19, Random.Range(0, 20));
                    spawnDir = Vector3.right;
                    break;
                case 3:
                    gridSpot = new Vector2(Random.Range(0, 20), 0);
                    spawnDir = Vector3.back;
                    break;
            }
            GridSquare tile = PlacementGrid.Instance.grid[(int)gridSpot.x, (int)gridSpot.y];

            if (!tile.built)
            {
                spawnCheck = true;
                CreateEnemy(tile, tile.worldPosition + spawnDir * spawnDistance);
            }
        }
    }

    private float GetWaveValue()
    {
        float val = 0f;
        switch (GameManager.Instance.difficulty)
        {
            case 0:
                if (currentWave <= 8) { val = WaveValueEasyBegin(); }
                else { val = WaveValueEasy(); }
                break;
            case 1:
                if (currentWave <= 8) { val = WaveValueMediumBegin(); }
                else { val = WaveValueMedium(); }
                break;
            case 2:
                if (currentWave <= 8) { val = WaveValueHardBegin(); }
                else { val = WaveValueHard(); }
                break;
        }

        if (val < 0.5f) { val = 0.5f; }
        return val;
    }

    private float WaveValueEasyBegin()
    {
        float val = Mathf.Log(currentWave + 1, 2) * 2;
        val = Mathf.Round(val) / 2f;
        return val;
    }
    private float WaveValueEasy()
    { return (currentWave * 0.5f - 4); }

    private float WaveValueMediumBegin()
    {
        float val = (Mathf.Log(currentWave, 2) + 1) * 2;
        val = Mathf.Round(val) / 2f;
        return val;
    }
    private float WaveValueMedium()
    { return (currentWave - 4); }

    private float WaveValueHardBegin()
    {
        float val = Mathf.Log(currentWave + 1, 2) * 4;
        val = Mathf.Round(val) / 2f;
        return val;
    }
    private float WaveValueHard()
    { return (currentWave * 1.5f - 4); }
}
