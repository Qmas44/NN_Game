using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyCounter : MonoBehaviour
{
    private int currentEnemies = 0;

    protected Text _text;

    protected virtual void Start()
    {
        _text = GetComponent<Text>();
    }

    public void IncreaseEnemyCount()
    {
        currentEnemies++;
        UpdateCounter();
    }

    public void DecreaseEnemyCount()
    {
        currentEnemies--;
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        Debug.Log("Current enemy count: " + currentEnemies);
        _text.text = "Enemy Count: " + currentEnemies;
    }
}