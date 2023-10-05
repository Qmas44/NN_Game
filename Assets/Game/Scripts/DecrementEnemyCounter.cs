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
        Debug.Log("Enemy counter: in death feedback" + _enemyCounter);
    }

    public void Decrement()
    {
        Debug.Log("decrement counter in death feedback");
        _enemyCounter.DecreaseEnemyCount();
    }

}
