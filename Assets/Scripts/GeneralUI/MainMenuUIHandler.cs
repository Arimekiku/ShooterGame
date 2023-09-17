using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    [Header("UI Player Stats")]
    [SerializeField] private Text TotalCoinsCount;
    [SerializeField] private Text TotalAttackDamage;
    [SerializeField] private Text TotalAttackSpeed;

    [Header("UI Level Stats")] 
    [SerializeField] private Slider TotalRoadCount;
    [SerializeField] private Slider TotalEnemyCount;
    [SerializeField] private Slider TotalEnemyReward;
    [SerializeField] private InputField EnemyHealth;
    [SerializeField] private InputField BossHealth;
    
    [Header("UI Panel Preferences")]
    [SerializeField] private DisposableUIPanel ShopPanel;
    [SerializeField] private DisposableUIPanel LevelBalancePanel;
    [SerializeField] private DisposableUIPanel StatsPanel;

    [Header("UI Shop Preferences")] 
    [SerializeField] private Text AttackDamagePrice;
    [SerializeField] private Text AttackSpeedPrice;

    [Header("UI Stats Preferences")] 
    [SerializeField] private Text LevelsPassed;
    [SerializeField] private Text EnemiesKilled;
    [SerializeField] private Text CoinsEarned;

    private UpgradeHandler _upgradeHandler;
    private SaveDataInfo _saveData;

    public void Init(UpgradeHandler newUpgradeHandler, MoneyHandler moneyHandler, SaveDataInfo newDataInfo)
    {
        ShopPanel.Dispose();
        LevelBalancePanel.Dispose();
        StatsPanel.Dispose();

        _upgradeHandler = newUpgradeHandler;
        _saveData = newDataInfo;
        
        moneyHandler.OnCoinsUpdate += UpdateCoinsCount;
        moneyHandler.UpdateCoins(_saveData.CurrentCoinsInGame);
        UpdateLevelInfo();
        UpdateShopInfo();
        UpdateStatsInfo();
    }
    
    private void UpdateCoinsCount(int count)
    {
        TotalCoinsCount.text = count.ToString();
    }

    private void UpdateLevelInfo()
    {
        TotalRoadCount.value = _saveData.RoadCount;
        TotalEnemyReward.value = _saveData.EnemyReward;
        TotalEnemyCount.value = _saveData.EnemyCount;
        EnemyHealth.text = _saveData.EnemyHealth.ToString();
        BossHealth.text = _saveData.BossHealth.ToString();
    }

    private void UpdateShopInfo()
    {
        AttackDamagePrice.text = UpgradeHandler.AttackDamageUpgradePrice.ToString();
        AttackSpeedPrice.text = UpgradeHandler.AttackSpeedUpgradePrice.ToString();
        
        TotalAttackSpeed.text = _saveData.AttackSpeed.ToString(CultureInfo.InvariantCulture);
        TotalAttackDamage.text = _saveData.AttackDamage.ToString();
    }

    private void UpdateStatsInfo()
    {
        LevelsPassed.text = _saveData.TotalLevelPassed.ToString();
        EnemiesKilled.text = _saveData.TotalEnemyKilled.ToString();
        CoinsEarned.text = _saveData.TotalCoinsEarned.ToString();
    }

    public void OnPlayButtonPressed()
    {
        _saveData.RoadCount = (int)TotalRoadCount.value;
        _saveData.EnemyCount = (int)TotalEnemyCount.value;
        _saveData.EnemyReward = (int)TotalEnemyReward.value;
        _saveData.EnemyHealth = int.Parse(EnemyHealth.text);
        _saveData.BossHealth = int.Parse(BossHealth.text);
        
        SceneManager.LoadScene((int)SceneIndexes.Level);
    }

    public void OnAttackSpeedButtonPressed()
    {
        _upgradeHandler.MakeAttackSpeedUpgrade(ref _saveData.AttackSpeed, ref _saveData.CurrentCoinsInGame);
        
        TotalAttackSpeed.text = _saveData.AttackSpeed.ToString(CultureInfo.InvariantCulture);
    }

    public void OnAttackDamageButtonPressed()
    {
        _upgradeHandler.MakeAttackDamageUpgrade(ref _saveData.AttackDamage, ref _saveData.CurrentCoinsInGame);

        TotalAttackDamage.text = _saveData.AttackDamage.ToString();
    }
}