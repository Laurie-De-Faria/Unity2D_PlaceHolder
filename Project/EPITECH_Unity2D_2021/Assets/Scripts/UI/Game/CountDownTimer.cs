using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private float currentTime;
    [SerializeField] private float startTime;

    [SerializeField] private PanelManager _panelManager;

    private void Start()
    {
        startTime = 660;
        currentTime = startTime;
    }

    private void Update()
    {
        if (!GameStatus.isPause)
        {
            if (currentTime <= 0)
            {
                //_panelManager.GameOver();
                GameStatus.isGameOver = true;
            }
            else
            {
                currentTime -= Time.deltaTime;
                _timerText.text = currentTime.ToString("0.0");
            }
        }
    }
}
