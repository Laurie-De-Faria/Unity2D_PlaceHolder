using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame () {
        SceneManager.LoadScene(1);

        
    }

    public void QuitGame() {
        Application.Quit();
    }

    List<int> widths = new List<int>() { 568, 960, 1280, 1920 };
    List<int> heights = new List<int>() { 329, 540, 800, 1080 };

    public void SetScreenSize(int index)
    {

        int width = widths[index];
        int height = heights[index];
        Logger.Log(width.ToString() +  height.ToString());

        
        Screen.SetResolution(width, height, false);
    }

}
