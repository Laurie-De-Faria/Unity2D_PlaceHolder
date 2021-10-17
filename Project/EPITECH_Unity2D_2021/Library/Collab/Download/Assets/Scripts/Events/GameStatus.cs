using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    #region Variables
    public static bool isPause;
    public static bool isGameOver;

    public delegate void GameStop();
    public static event GameStop OnGamePause;
    public delegate void GameOver();
    public static event GameOver OnGameOver;
    #endregion
    private void Start()
    {
        isPause = true;
        isGameOver = false;
        OnGameOver += ResetGameOver;
    }

    private void Update()
    {
        if (isPause && OnGamePause != null)
        {
            OnGamePause.Invoke();
        }
        if (isGameOver && OnGameOver != null)
        {
            OnGameOver.Invoke();
        }
    }

    private void ResetGameOver()
    {
        isGameOver = false;
    }

    private void OnDestroy()
    {
        OnGameOver -= ResetGameOver;
    }
}
