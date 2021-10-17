using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
    public static void Log(string msg)
    {
        Debug.Log("<color=grey>[LOG]</color> " + msg);
    }

    public static void LogWarning(string msg)
    {
        Debug.LogWarning("<color=orange>[WARNING]</color> " + msg);
    }

    public static void LogError(string msg)
    {
        Debug.LogError("<color=red>[ERROR]</color> " + msg);
    }
}
