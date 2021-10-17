using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    private int CoinNumber = 0;
    [SerializeField] private Text CoinText;

    private void Start()
    {
        CoinText.text = CoinNumber.ToString();
    }

    public void AddCoin()
    {
        CoinNumber++;
        CoinText.text = CoinNumber.ToString();
    }
}
