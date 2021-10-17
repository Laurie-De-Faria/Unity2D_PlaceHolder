using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    #region Variables
    #region Sounds
    [Header("Attacks sounds:")]
    [SerializeField] private AudioSource _meleeAttack;
    [SerializeField] private AudioSource _rangedAttack;
    #endregion
    #endregion

    #region Methods
    #region Attacks
    public void StartMeleeAttack()
    {
        _meleeAttack.Play();
    }

    public void StartRangedAttack()
    {
        _rangedAttack.Play();
    }
    #endregion
    #endregion
}
