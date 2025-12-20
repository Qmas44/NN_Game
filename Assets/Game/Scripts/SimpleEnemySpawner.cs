using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject RegularEnemyPrefab;

    [Tooltip("The distance the enemy spawns from the player")]
    [SerializeField] private float SpawnDistance = 10f;

    private GameObject currentEnemy;




    // Start is called before the first frame update
    void Start()
    {
        SpawnInitialEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        // Check if current enemy is dead and spawn a new one
        if (currentEnemy == null || !currentEnemy.activeInHierarchy)
        {
            SpawnRegularEnemy();
        }
    }

    void SpawnInitialEnemy()
    {
        Vector3 RandomPosition = GetRandomPosition();
        currentEnemy = Instantiate(RegularEnemyPrefab, RandomPosition, Quaternion.identity);
    }

    void SpawnRegularEnemy()
    {
        Vector3 RandomPosition = GetRandomPosition();
        currentEnemy = Instantiate(RegularEnemyPrefab, RandomPosition, Quaternion.identity);
    }


    Vector3 GetRandomPosition()
    {
        Vector3 PlayerPosition = _player.transform.position;
        Vector3 RandomOffset = Random.insideUnitSphere * SpawnDistance;
        RandomOffset.y = 0f;
        return PlayerPosition + RandomOffset;
    }
}
