using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] regularEnemyPrefab;
    [SerializeField] private GameObject[] eliteEnemyPrefab;

    [Tooltip("The distance the enemy spawns from the player")]
    [SerializeField] private float spawnDistance = 10f;
    [SerializeField] private int maxRegularEnemies = 5;
    [SerializeField] private int maxEliteEnemies = 2;
    [SerializeField] private int maxRegularEnemiesPerWave = 3; // Maximum regular enemies spawned per wave
    [SerializeField] private int maxEliteEnemiesPerWave = 1; // Maximum elite enemies spawned per wave

    [SerializeField] private int regularEnemiesSpawnedPerWave = 0; // Number of enemies spawned in this wave
    [SerializeField] private int eliteEnemiesSpawnedPerWave = 0; // Number of enemies spawned in this wave

    [SerializeField] private float eliteSpawnProbability = 0.25f; // Base probability of spawning an elite enemy
    [SerializeField] private float[] stageEliteSpawnProbabilities; // Array of probabilities for spawning elite enemies at different stages
    [SerializeField] private float waveInterval = 10f; // Interval between enemy waves
    [SerializeField] private float waveTimer = 0f;
    [SerializeField] private int currentRegularEnemies;
    [SerializeField] private int currentEliteEnemies;
    [SerializeField] private int currentStage = 0; // Current stage of the level

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); // need to refactor here to get player prefab not in update 

        waveTimer += Time.deltaTime;

        if (waveTimer >= waveInterval)
        {
            SpawnWave();

            waveTimer = 0f;
        }

    }

    void SpawnWave()
    {
        SpawnRegularEnemies();
        SpawnEliteEnemies();
    }

    void SpawnRegularEnemies()
    {
        while (currentRegularEnemies < maxRegularEnemies)
        {
            Vector3 randomPosition = GetRandomPosition();

            GameObject randomRegularEnemyPrefab = regularEnemyPrefab[Random.Range(0, regularEnemyPrefab.Length)];

            GameObject newEnemy = Instantiate(randomRegularEnemyPrefab, randomPosition, Quaternion.identity);

            regularEnemiesSpawnedPerWave++;
            currentRegularEnemies++;

            if (regularEnemiesSpawnedPerWave >= maxRegularEnemiesPerWave)
            {
                regularEnemiesSpawnedPerWave = 0;
                break;
            }

        }
    }

    void SpawnEliteEnemies()
    {
        while (currentEliteEnemies < maxEliteEnemies)
        {
            if (Random.value < GetEliteSpawnProbability())
            {
                Vector3 randomPosition = GetRandomPosition();

                GameObject randomEliteEnemyPrefab = eliteEnemyPrefab[Random.Range(0, eliteEnemyPrefab.Length)];
                
                Debug.Log("Spawning elite enemy at " + randomPosition);
                GameObject newEnemy = Instantiate(randomEliteEnemyPrefab, randomPosition, Quaternion.identity);

                eliteEnemiesSpawnedPerWave++;
                currentEliteEnemies++;

                if (eliteEnemiesSpawnedPerWave >= maxEliteEnemiesPerWave)
                {
                    eliteEnemiesSpawnedPerWave = 0;
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }

    float GetEliteSpawnProbability()
    {
        if (stageEliteSpawnProbabilities != null && stageEliteSpawnProbabilities.Length > currentStage)
        {
            return eliteSpawnProbability * stageEliteSpawnProbabilities[currentStage];
        }
        else
        {
            return eliteSpawnProbability; // Return base probability if no stage probabilities are set
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 randomOffset = Random.insideUnitSphere * spawnDistance;
        randomOffset.y = 0f;
        return playerPosition + randomOffset;
    }

    public void SetStage(int stage)
    {
        currentStage = stage;
    }
}
