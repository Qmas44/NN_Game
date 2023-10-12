using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecrementEnemyCounter : MonoBehaviour
{
    private EnemyCounter _enemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        _enemyCounter = GameObject.Find("EnemyCounter").GetComponent<EnemyCounter>();
    }

    public void Decrement()
    {
        _enemyCounter.DecreaseEnemyCount();
    }

}
