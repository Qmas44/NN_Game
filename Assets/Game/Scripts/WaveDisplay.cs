using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveDisplay : MonoBehaviour
{
    private int Wave = 0;

    public PointsBasedWaveSpawner WaveSpawner;

    protected Text _text;



    protected virtual void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _text.text = "Wave " + WaveSpawner.CurrWave;
    }
}