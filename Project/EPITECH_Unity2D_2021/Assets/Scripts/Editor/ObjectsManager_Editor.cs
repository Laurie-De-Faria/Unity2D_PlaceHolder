using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(ObjectsManager))]
public class ObjectsManager_Editor : Editor
{
    private Vector2 _position;
    //private CollectableObject _object;
    private int _id;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectsManager myScript = (ObjectsManager)target;
        _position = EditorGUILayout.Vector2Field("Position to create object:", _position);
        //_object = (CollectableObject) EditorGUILayout.ObjectField(_object, Type.GetType("CollectableObject"));
        _id = EditorGUILayout.IntField("Id of object to instantiate:", 0);
        if (GUILayout.Button("Create object"))
        {
            myScript.InstantiateCollectableObject(_position, _id);
        }
    }
}
