using UnityEngine;

public class UpgradeHandler
{
    public const int AttackDamageUpgradePrice = 10;
    public const int AttackSpeedUpgradePrice = 15;
    
    private const int AttackDamageUpgradeAmount = 1;
    private const float AttackSpeedUpgradeAmount = 0.1f;
    
    private readonly MoneyHandler _moneyHandler;
    
    public UpgradeHandler(MoneyHandler newMoneyHandler)
    {
        if (!PlayerPrefs.HasKey(SaveKeyTemplates.AttackDamageKey))
            PlayerPrefs.SetInt(SaveKeyTemplates.AttackDamageKey, 1);

        if (!PlayerPrefs.HasKey(SaveKeyTemplates.AttackSpeedKey))
            PlayerPrefs.SetFloat(SaveKeyTemplates.AttackSpeedKey, 1);
        
        _moneyHandler = newMoneyHandler;
    }

    public void MakeUpgrade(string key)
    {
        if (!PlayerPrefs.HasKey(key))
            throw new("Wrong key entered");

        switch (key)
        {
            case SaveKeyTemplates.AttackDamageKey:
                if (_moneyHandler.TryDepleteCoins(AttackDamageUpgradePrice))
                {
                    int attackDamageAmount = PlayerPrefs.GetInt(SaveKeyTemplates.AttackDamageKey);
                    PlayerPrefs.SetInt(SaveKeyTemplates.AttackDamageKey, attackDamageAmount + AttackDamageUpgradeAmount);
                }
                
                break;
            case SaveKeyTemplates.AttackSpeedKey:
                if (_moneyHandler.TryDepleteCoins(AttackSpeedUpgradePrice))
                {
                    float attackSpeedAmount = PlayerPrefs.GetFloat(SaveKeyTemplates.AttackSpeedKey);
                    PlayerPrefs.SetFloat(SaveKeyTemplates.AttackSpeedKey, attackSpeedAmount + AttackSpeedUpgradeAmount);
                }
                
                break;
        }
    }
}