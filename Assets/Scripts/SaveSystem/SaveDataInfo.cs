using UnityEngine;

[System.Serializable]
public class SaveDataInfo
{
    [Header("Player Preferences")] 
    public int AttackDamage = 1;
    public float AttackSpeed = 1;

    [Header("Level Preferences")] 
    public int EnemyCount = 5;
    public int RoadCount = 4;
    
    [Header("Enemy Preferences")]
    public int EnemyReward = 2;
    public int EnemyHealth = 2;
    public int BossHealth = 4;
    
    [Header("Economic Preferences")]
    public int CurrentCoinsInGame;

    [Header("Stats")] 
    public int TotalCoinsEarned;
    public int TotalLevelPassed;
    public int TotalEnemyKilled;
}