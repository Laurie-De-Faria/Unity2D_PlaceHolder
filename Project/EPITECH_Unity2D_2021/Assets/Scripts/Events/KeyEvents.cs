using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvents : MonoBehaviour
{
    #region Keyboard
    public delegate void PressZ();
    public static event PressZ OnPressZ;
    public delegate void PressQ();
    public static event PressQ OnPressQ;
    public delegate void PressD();
    public static event PressD OnPressD;

    public delegate void PressSpace();
    public static event PressSpace OnPressSpace;

    public delegate void AnyKeyUp();
    public static event AnyKeyUp OnAnyKeyUp;
    #endregion

    #region Mouse
    public delegate void ClickMouseLeft();
    public static event ClickMouseLeft OnClickMouseLeft;
    public delegate void ClickMouseRight();
    public static event ClickMouseRight OnClickMouseRight;
    #endregion

    private void Update()
    {
        if (!GameStatus.isPause)
            EventsInGame();
        else
            EventsInPause();
    }

    /// <summary>
    /// Events to active when the game is pause.
    /// </summary>
    private void EventsInPause()
    {

    }

    /// <summary>
    /// Events to active when the game is run.
    /// </summary>
    private void EventsInGame()
    {
        if (Input.GetKeyDown(KeyCode.Z) && OnPressZ != null)
            OnPressZ.Invoke();
        if (Input.GetKey(KeyCode.Q) && OnPressQ != null)
            OnPressQ.Invoke();
        if (Input.GetKey(KeyCode.D) && OnPressD != null)
            OnPressD.Invoke();

        if (Input.GetKeyDown(KeyCode.Space) && OnPressSpace != null)
            OnPressSpace.Invoke();

        if ((Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.D)) && OnAnyKeyUp != null)
            OnAnyKeyUp.Invoke();

        if (Input.GetMouseButtonDown(0) && OnClickMouseLeft != null)
            OnClickMouseLeft.Invoke();
        if (Input.GetMouseButtonDown(1) && OnClickMouseRight != null)
            OnClickMouseRight.Invoke();
    }
}
