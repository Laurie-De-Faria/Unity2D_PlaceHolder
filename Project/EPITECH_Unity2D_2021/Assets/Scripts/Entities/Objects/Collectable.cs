using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    #region Variables
    public int id { get; private set; }
    public string objectName { get; private set; }
    #endregion

    #region Methods
    public void InitCollectable(CollectableObject obj)
    {
        id = obj._id;
        objectName = obj._name;
    }

    public void InitCollectable(int id, string name)
    {
        this.id = id;
        objectName = name;
    }
    #endregion
}
