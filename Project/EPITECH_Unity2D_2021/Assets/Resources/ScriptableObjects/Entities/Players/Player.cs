using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Entities/Player")]
public class Player : ScriptableObject
{
    public int _id;
    public string _name;
    public PlayerEnums.RACE _race;
    public PlayerEnums.CLASS _class;

    public float _life;
    public float _attack;
    public float _defence;
    public float _speed;
    
    public Sprite _sprite;
    public GameObject _prefab;
}
