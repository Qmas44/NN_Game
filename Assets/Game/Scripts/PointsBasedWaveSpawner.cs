using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsBasedWaveSpawner : MonoBehaviour
{
    public List<Enemy> Enemies = new List<Enemy>();
    public int CurrWave;
    /// If this is true, this confiner will listen to set confiner events
		[Tooltip("Wave value is currWave * 10")]
    public int WaveValue;

    public List<GameObject> EnemiesToSpawn = new List<GameObject>();

    public List<Transform> SpawnLocations = new List<Transform>();
    public int WaveDuration;

    //Make private later
    public float WaveTimer;
    public float SpawnInverval;
    public float SpawnTimer;

    public List<GameObject> SpawnedEnemies = new List<GameObject>(); // Keep track of enemies spawned to start next wave


    void Start()
    {
        GenerateWave();
    }

    void FixedUpdate()
    {
        if(SpawnTimer <= 0)
        {
            //spawn enemy
            if(EnemiesToSpawn.Count > 0)
            {
                Transform SpawnLocation = SpawnLocations[Random.Range(0, SpawnLocations.Count)]; // get random spawn location
                GameObject Enemy =Instantiate(EnemiesToSpawn[0], SpawnLocation.position, Quaternion.identity); // Spawn first enemy in list
                EnemiesToSpawn.RemoveAt(0); // remove enemy from list
                SpawnedEnemies.Add(Enemy);
                SpawnTimer = SpawnInverval;
            }
            else
            {
                WaveTimer = 0;
            }

        }
        else
        {
            SpawnTimer -= Time.fixedDeltaTime;
            WaveTimer -= Time.fixedDeltaTime;
        }

        if(WaveTimer <= 0 && SpawnedEnemies.Count <= 0)
        {
            CurrWave++;
            GenerateWave();
        }
    }

    // May want to find a better way to increase the difficulty (Maybe removing low cost enemies or modifying how fast the enemies spawn(Use Wave Duration))
    public void GenerateWave()
    {
        WaveValue = CurrWave * 10; // WAVE VALUE sets how dificult the wave is (Really just sets how many enemies can spawn)
        GenerateEnemies();

        SpawnInverval = WaveDuration / EnemiesToSpawn.Count; //Gives a fixed times between each enemys spawn
        WaveTimer = WaveDuration;

    }
    
    public void GenerateEnemies()
    {
        List<GameObject> GeneratedEnemies = new List<GameObject>();
        while(WaveValue>0)
        {
            int RandEnemyID = Random.Range(0, Enemies.Count);
            int RandEnemyCost = Enemies[RandEnemyID].Cost;

            if(WaveValue-RandEnemyCost >= 0)
            {
                GeneratedEnemies.Add(Enemies[RandEnemyID].EnemyPrefab);
                WaveValue -= RandEnemyCost;
            }
            else if (WaveValue <= 0)
            {
                break;
            }
        }
        EnemiesToSpawn.Clear();
        EnemiesToSpawn = GeneratedEnemies;

    }

    public void RemoveEnemy()
    {
        Debug.Log("REMOVING ENEMY");
        SpawnedEnemies.RemoveAt(0);
    }



}

[System.Serializable]
public class Enemy
{
    public GameObject EnemyPrefab;
    public int Cost;
}
