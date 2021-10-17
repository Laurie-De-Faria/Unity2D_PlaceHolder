using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private Image _playerDisplay;

    public void ChargePlayerSprite(Sprite playerSprite)
    {
        if (playerSprite == null)
            Logger.LogWarning("Sprite set on player display is null.");
        _playerDisplay.sprite = playerSprite;
    }
}
