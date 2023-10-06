using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelProgressUI : MonoBehaviour
{
    [Header("UI references:")]
    [SerializeField] private Image progressBar;
    [SerializeField] private Text levelText;

    public void UpdateProgressBar(int experience)
    {
        float value = (float)experience / 300;
        progressBar.fillAmount += value;
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = "LVL " + level;
    }

    public void ResetProgress()
    {
        progressBar.fillAmount = 0;
    }
    public void ResetLevelText()
    {
        levelText.text = "LVL 1";
    }
    public float GetProgress()
    {
        Debug.Log(progressBar.fillAmount);
        return progressBar.fillAmount;
    }
}
