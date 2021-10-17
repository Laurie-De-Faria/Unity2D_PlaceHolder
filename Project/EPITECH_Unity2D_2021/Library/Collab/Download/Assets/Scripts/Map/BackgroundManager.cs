using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Backgrounds = new List<GameObject>();

    private int index = 0;

    private void Start()
    {
        foreach (GameObject Background in Backgrounds)
        {
            Background.SetActive(false);
        }
        if(Backgrounds.Count != 0)
        {
            Backgrounds[0].SetActive(true);
        }
        LevelEvent.OnNextLevel += ChangeBackground;
    }

    public void ChangeBackground()
    {
        Logger.Log("BackgroundChange");
        index++;
        if(index > 0)
        {
            Backgrounds[index - 1].SetActive(false);
        }
        if (index < Backgrounds.Count)
        {
            Logger.Log("BackgroundChange++");
            Backgrounds[index].SetActive(true);
        }
    }

    private void OnDestroy()
    {
        LevelEvent.OnNextLevel -= ChangeBackground;
    }
}
