using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public TextMeshProUGUI textMeshPro;
    public int coins;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        UpdateCoinsGUI();
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        UpdateCoinsGUI();
    }

    public void UpdateCoinsGUI()
    {
        textMeshPro.text = $"X{coins}";
    }
}
