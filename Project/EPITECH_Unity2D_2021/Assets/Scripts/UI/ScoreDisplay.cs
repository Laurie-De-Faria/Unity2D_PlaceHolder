using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private int ScoreNumber = 0;
    [SerializeField] private Text ScoreText;

    private void Start()
    {
        ScoreText.text = "Score : " + ScoreNumber.ToString();
    }

    public void AddScore(int score)
    {
        ScoreNumber = ScoreNumber + score;
        ScoreText.text = "Score : " + ScoreNumber.ToString();
    }
}
