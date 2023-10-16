using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    private int score = 0;

    protected Text _text;

    protected virtual void Start()
    {
        _text = GetComponent<Text>();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScore();
    }

    public void UpdateScore()
    {
        _text.text = "Score " + score;
    }
}