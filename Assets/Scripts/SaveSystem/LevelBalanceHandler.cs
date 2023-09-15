using UnityEngine;

public class LevelBalanceHandler
{
    private const int DefaultEnemyCount = 5;
    private const int DefaultEnemyReward = 2;
    private const int DefaultEnemyHealth = 2;
    private const int DefaultBossHealth = 4;
    private const int DefaultRoadCount = 3;
    
    public LevelBalanceHandler()
    {
        if (!PlayerPrefs.HasKey(SaveKeyTemplates.RoadCountKey))
            PlayerPrefs.SetInt(SaveKeyTemplates.RoadCountKey, DefaultRoadCount);
        
        if (!PlayerPrefs.HasKey(SaveKeyTemplates.EnemyCountKey))
            PlayerPrefs.SetInt(SaveKeyTemplates.EnemyCountKey, DefaultEnemyCount);
        
        if (!PlayerPrefs.HasKey(SaveKeyTemplates.EnemyRewardKey))
            PlayerPrefs.SetInt(SaveKeyTemplates.EnemyRewardKey, DefaultEnemyReward);
        
        if (!PlayerPrefs.HasKey(SaveKeyTemplates.EnemyHealthKey))
            PlayerPrefs.SetInt(SaveKeyTemplates.EnemyHealthKey, DefaultEnemyHealth);
        
        if (!PlayerPrefs.HasKey(SaveKeyTemplates.BossHealthKey))
            PlayerPrefs.SetInt(SaveKeyTemplates.BossHealthKey, DefaultBossHealth);
    }
    
    public void ApplyNewBalanceValue(int newCount, string key)
    {
        if (!PlayerPrefs.HasKey(key))
            throw new("Invalid key received");
            
        PlayerPrefs.SetInt(key, newCount);
    }
}