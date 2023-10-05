using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private Score _score;

    // Start is called before the first frame update
    void Start()
    {
        _score = GameObject.Find("Score").GetComponent<Score>();
    }

    public void Increment()
    {
        _score.IncreaseScore(10);
    }

}
