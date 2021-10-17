using UnityEngine;

[CreateAssetMenu(fileName = "Projectiles", menuName = "Entities/Projectile")]
public class Projectiles : ScriptableObject
{
    public int _id;
    public string _name;
    public Sprite _sprite;
    public ProjectilesEnums.TYPE _type;

    public float _attack;
    public float _speed;
}
