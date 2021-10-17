using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Stuff/Armor")]
public class Armor : ScriptableObject
{
    public int _id;
    public string _name;
    public Sprite _sprite;

    public List<PieceOfArmor> _pieces;

    //public float _defence = CalculDefence();

    public static float CalculDefence(Armor armor)
    {
        float valueDefence = 0;

        foreach (PieceOfArmor piece in armor._pieces)
            valueDefence += piece._defence;
        return valueDefence;
    }
}