using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEvent : MonoBehaviour
{
    public static bool Changelevel;
    public delegate void NextLevel();
    public static event NextLevel OnNextLevel;

    private void Start()
    {
        OnNextLevel += ResetBool;
        Changelevel = false;
    }

    void Update()
    {
       if(Changelevel && OnNextLevel != null)
        {
            Logger.Log("Debug");
            OnNextLevel.Invoke();
        }
    }

    private void ResetBool()
    {
        Changelevel = false;
    }

    private void OnDestroy()
    {
        OnNextLevel -= ResetBool;
    }
}
