using UnityEngine;

[CreateAssetMenu(fileName = "PieceOfArmor", menuName = "Stuff/PieceOfArmor")]
public class PieceOfArmor : ScriptableObject
{
    public int _id;
    public string _name;
    public Sprite _sprite;
    public StuffEnums.TYPE_PIECE_OF_ARMOR _type;

    public float _defence;
}