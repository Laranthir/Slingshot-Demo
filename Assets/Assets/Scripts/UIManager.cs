using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject scoreUI;
    
    private TMP_Text tmp_Score;

    private int score;

    private void Start()
    {
        tmp_Score = scoreUI.GetComponent<TMP_Text>();
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        tmp_Score.text = score.ToString();
    }
}
