using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomPlayer : MonoBehaviour
{
    #region Variables
    [SerializeField] private PlayerControl _player;

    [SerializeField] private List<GameObject> _selectionCategories;
    private int indexSelection = 0;
    #endregion

    #region Methods
    #region Initialization
    private void Start()
    {
        InitSelection();
    }

    private void InitSelection()
    {
        foreach (GameObject categorie in _selectionCategories)
        {
            categorie.SetActive(false);
        }
        _selectionCategories[0].SetActive(true);
    }
    #endregion

    #region Charge parameters
    public void ChargeCharacter(Player character)
    {
        _player.InitPlayer(character);
        SwitchSelectionCategories();
    }

    public void ChargeArmor(Armor armor)
    {
        _player.SetArmor(armor);
        SwitchSelectionCategories();
    }

    public void ChargeWeapon(Weapon weapon)
    {
        _player.SetWeapon(weapon);
        SwitchSelectionCategories();
    }

    public void ChargeProjectile(Projectiles projectile)
    {
        _player.SetProjectile(projectile);
        SwitchSelectionCategories();
    }
    #endregion

    #region Change selection
    public void SwitchSelectionCategories()
    {
        _selectionCategories[indexSelection].SetActive(false);
        indexSelection++;
        if (indexSelection < _selectionCategories.Count())
            _selectionCategories[indexSelection].SetActive(true);
        else
        {
            HidePanel();
        }
    }
    #endregion

    #region Manage panel
    private void HidePanel()
    {
        GameStatus.isPause = false;
        gameObject.SetActive(false);
    }
    #endregion
    #endregion
}
