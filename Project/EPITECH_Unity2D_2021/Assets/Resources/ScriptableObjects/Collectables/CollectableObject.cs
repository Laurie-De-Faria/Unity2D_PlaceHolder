using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableObject", menuName = "Object/Collectable")]
public class CollectableObject : ScriptableObject
{
    public int _id;
    public string _name;
    public Sprite _sprite;
    public GameObject _prefab;

    public ObjectsEnums.BONUS_EFFECT _bonusEffect;
    public float _bonusValue;
    public float _bonusDuration;
    public ObjectsEnums.MALUS_EFFECT _malusEffect;
    public float _malusValue;
    public float _malusDuration;

    public StuffEnums.TYPE_TOOLS _toolToCollect;
}