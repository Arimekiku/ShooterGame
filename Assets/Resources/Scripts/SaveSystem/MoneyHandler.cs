using System;
using UnityEngine;

public class MoneyHandler
{
    public event Action<int> OnCoinsUpdate;
    
    private int _totalCoins;

    public MoneyHandler()
    {
        if (PlayerPrefs.HasKey(SaveKeyTemplates.CurrentCoinsKey))
        {
            _totalCoins = PlayerPrefs.GetInt(SaveKeyTemplates.CurrentCoinsKey);
        }
        else
        {
            PlayerPrefs.SetInt(SaveKeyTemplates.CurrentCoinsKey, 0);
        }
    }

    public void UpdateCoins()
    {
        OnCoinsUpdate.Invoke(_totalCoins);
    }

    public bool TryDepleteCoins(int count)
    {
        if (_totalCoins < count)
            return false;

        _totalCoins -= count;
        PlayerPrefs.SetInt(SaveKeyTemplates.CurrentCoinsKey, _totalCoins);
        UpdateCoins();
        
        return true;
    }
}