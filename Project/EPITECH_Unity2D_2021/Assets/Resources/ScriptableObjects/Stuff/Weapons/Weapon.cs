using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Stuff/Weapon")]
public class Weapon : ScriptableObject
{
    public int _id;
    public string _name;
    public Sprite _sprite;
    public ProjectilesEnums.TYPE _type;

    public float _attack;
}
