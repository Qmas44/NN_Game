using UnityEngine;

public class AIEnemyGenerator : MonoBehaviour
{
    [Tooltip("The enemy prefab to spawn")]
    [SerializeField] private GameObject[] enemyPrefabs;

    private EnemyCounter _enemyCounter;

    [Tooltip("The player prefab to centralize the enemy spawns around")]
    [SerializeField] private GameObject playerPrefab;

    [Tooltip("The time interval between enemy spawns")]
    [SerializeField] private float spawnInterval = 5f;

    [Tooltip("The distance the enemy spawns from the player")]
    [SerializeField] private float spawnDistance = 10f;

    [Tooltip("The number of enemies to spawn")]
    [SerializeField] private int numberOfEnemies = 1;

    [Tooltip("The max number of enemies to spawn at once")]
    [SerializeField] private int maxEnemyCount = 50;

    private float timer;

    private Camera mainCamera;

    private void Start()
    {
        timer = spawnInterval;

        _enemyCounter = GameObject.Find("EnemyCounter").GetComponent<EnemyCounter>();

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            return;
        }

        playerPrefab = GameObject.FindGameObjectWithTag("Player"); // need to refactor here to get player prefab not in update 

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if(_enemyCounter.GetEnemyCount() < maxEnemyCount)
            {
                SpawnEnemies();
            }
            timer = spawnInterval;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 1f);

            
            if (colliders.Length == 0 && !IsInView(spawnPosition))
            {
                GameObject mob = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.identity);

                mob.SetActive(true);
                _enemyCounter.IncreaseEnemyCount();

            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 playerPosition = playerPrefab.transform.position;
        Vector3 randomOffset = Random.insideUnitSphere * spawnDistance;
        randomOffset.y = 0f;
        return playerPosition + randomOffset;
    }

    private bool IsInView(Vector3 position)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(position);
        return viewportPoint.x >= 0f && viewportPoint.x <= 1f && viewportPoint.y >= 0f && viewportPoint.y <= 1f && viewportPoint.z > 0f;
    }
}