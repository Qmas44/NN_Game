using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyWaveTracker : MonoBehaviour
{

    public void RemoveEnemy()
    {
        if (GameObject.FindGameObjectWithTag("EnemyManager") != null)
        {
            Debug.Log("Removed enemy game object");
            GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<PointsBasedWaveSpawner>().RemoveEnemy();
        }
     
    }

}