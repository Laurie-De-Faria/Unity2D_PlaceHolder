using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [Header("Panels:")]
    [SerializeField] private GameObject PanelGameover;

    [Header("Scripts:")]
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private CoinDisplay coinDisplay;
    [SerializeField] private ScoreDisplay scoreDisplay;


    /*    private void Start()
        {
            InitEvents();
        }

        private void InitEvents()
        {
            GameStatus.OnGameOver += GameOver;
        }

        private void ResetEvents()
        {
            GameStatus.OnGameOver -= GameOver;
        }*/

    public void HealthUpdate(float life)
    {
        healthDisplay.LowerHealth(life);
    }
    public void CoinUpdate()
    {
        coinDisplay.AddCoin();
    }

    public void ScoreUpdate(int score)
    {
        scoreDisplay.AddScore(score);
    }

    public void GameOver()
    {
        PanelGameover.SetActive(true);
        //GameStatus.isGameOver = true;
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    public static void LoadMap()
    {
        SceneManager.LoadScene("Map");

    }

    /*private void OnDestroy()
    {
        ResetEvents();
    }*/
}
