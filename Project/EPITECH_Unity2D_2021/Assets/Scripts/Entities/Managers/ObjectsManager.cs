using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    #region Variables
    private Dictionary<int, CollectableObject> _collectablesObjects;

    [Header("Objects:")]
    [SerializeField] private Transform _objectsParent;
    #endregion

    #region Methods
    #region Initialization
    void Start()
    {
        InitCollectableObjDico();
    }

    private void InitCollectableObjDico()
    {
        Object[] objs = Resources.LoadAll("ScriptableObjects/Collectables", typeof(CollectableObject));
        _collectablesObjects = new Dictionary<int, CollectableObject>();

        foreach (CollectableObject obj in objs)
        {
            _collectablesObjects.Add(obj._id, obj);
        }
    }
    #endregion

    #region Instantiate collectable object
    #region Unique
    /// <summary>
    /// Instantiate collectable object in game.
    /// </summary>
    /// <param name="id">Id of object to instantiate.</param>
    /// <param name="name">Name of object to instantiate.</param>
    /// <param name="obj">GameObject representing object to instantiate.</param>
    /// <param name="position">Position to instantiate object.</param>
    private void InstantiateCollectableObject(int id, string name, GameObject obj, Vector2 position)
    {
        GameObject newObj;
        Collectable objInfos;

        if (obj is null)
            Logger.LogError("No object to instantiate.");
        else {
            newObj = Instantiate(obj, _objectsParent);
            newObj.transform.position = position;
            objInfos = newObj.GetComponent<Collectable>();
            objInfos.InitCollectable(id, name);
        }
    }

    /// <summary>
    /// Instantiate a collectable object in game.
    /// </summary>
    /// <param name="position">Position to instantiate object.</param>
    /// <param name="id">Id of object to instantiate.</param>
    public void InstantiateCollectableObject(Vector2 position, int id)
    {
        CollectableObject obj;

        if (!_collectablesObjects.ContainsKey(id))
            Logger.LogError("No object with this id.");
        else {
            obj = _collectablesObjects[id];
            InstantiateCollectableObject(obj._id, obj._name, obj._prefab, position);
        }
    }

    /// <summary>
    /// Instantiate a collectable object in game.
    /// </summary>
    /// <param name="position">Position to instantiate object.</param>
    /// <param name="obj">ScriptableObject of object to instantiate.</param>
    public void InstantiateCollectableObject(Vector2 position, CollectableObject obj)
    {

    }
    #endregion

    #region Multiple
    /// <summary>
    /// Instantiate lot of entity of same collectable object in game.
    /// </summary>
    /// <param name="positions">Positions to instantiate object.</param>
    /// <param name="id"></param>
    public void InstantiateCollectableObject(List<Vector2> positions, int id)
    {
        foreach (Vector2 position in positions)
            InstantiateCollectableObject(position, id);
    }

    /// <summary>
    /// Instantiate lot of entity of same collectable object in game.
    /// </summary>
    /// <param name="positions">Positions to instantiate object.</param>
    /// <param name="obj">ScriptableObject of object to instantiate.</param>
    public void InstantiateCollectableObject(List<Vector2> positions, CollectableObject obj)
    {
        foreach (Vector2 position in positions)
            InstantiateCollectableObject(position, obj);
    }

    /// <summary>
    /// Instantiate lot of collectable objects in game.
    /// </summary>
    /// <param name="positions">Positions to instantiate objects.</param>
    /// <param name="ids">Ids of objects to instantiate.</param>
    public void InstantiateCollectableObject(List<Vector2> positions, List<int> ids)
    {
        int index = 0;

        if (positions is null || ids is null)
        {
            Logger.LogError("No position or no object to instantiate.");
            return;
        }
        foreach (int id in ids)
        {
            if (index >= positions.Count())
            {
                Logger.LogWarning("No position to instantiate object.");
                break;
            }
            InstantiateCollectableObject(positions[index], id);
            index++;
        }
    }

    /// <summary>
    /// Instantiate lot of collectable objects in game.
    /// </summary>
    /// <param name="positions">Positions to instantiate objects.</param>
    /// <param name="objs">ScriptableObjects of objects to instantiate.</param>
    public void InstantiateCollectableObject(List<Vector2> positions, List<CollectableObject> objs)
    {
        int index = 0;

        if (positions is null || objs is null)
        {
            Logger.LogError("No position or no object to instantiate.");
            return;
        }
        foreach (CollectableObject obj in objs)
        {
            if (index >= positions.Count())
            {
                Logger.LogWarning("No position to instantiate object.");
                break;
            }
            InstantiateCollectableObject(positions[index], obj);
            index++;
        }
    }
    #endregion
    #endregion
    #endregion
}
