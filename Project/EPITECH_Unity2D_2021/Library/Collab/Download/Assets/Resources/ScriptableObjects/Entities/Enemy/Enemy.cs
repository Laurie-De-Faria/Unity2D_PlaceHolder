using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Entities/Enemy")]
public class Enemy : ScriptableObject
{
    public int _id;
    public string _name;

    public float _life;
    public float _attack;
    public float _defence;
}
