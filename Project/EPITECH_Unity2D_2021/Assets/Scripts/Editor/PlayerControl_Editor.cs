using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerControl))]
public class PlayerControl_Editor : Editor
{
    private float damages = 0f;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerControl myScript = (PlayerControl)target;
        if (GUILayout.Button("Kill player"))
        {
            myScript.Die();
        }
        damages = EditorGUILayout.FloatField("Damages to inflict:", damages);
        if (GUILayout.Button("Hurt player"))
        {
            myScript.ReceiveDamages(damages);
        }
    }
}
