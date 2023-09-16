using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    [Header("UI Player Stats")]
    [SerializeField] private Text _totalCoinsCount;
    [SerializeField] private Text _totalAttackDamage;
    [SerializeField] private Text _totalAttackSpeed;

    [Header("UI Level Stats")] 
    [SerializeField] private Slider _totalRoadCount;
    [SerializeField] private Slider _totalEnemyCount;
    [SerializeField] private Slider _totalEnemyReward;
    [SerializeField] private InputField _enemyHealth;
    [SerializeField] private InputField _bossHealth;
    
    [Header("UI Panel Preferences")]
    [SerializeField] private DisposableUIPanel _shopPanel;
    [SerializeField] private DisposableUIPanel _levelBalancePanel;
    [SerializeField] private DisposableUIPanel _statsPanel;

    [Header("UI Shop Preferences")] 
    [SerializeField] private Text _attackDamagePrice;
    [SerializeField] private Text _attackSpeedPrice;

    [Header("UI Stats Preferences")] 
    [SerializeField] private Text _levelCount;
    [SerializeField] private Text _enemiesCount;
    [SerializeField] private Text _coinsCount;

    private UpgradeHandler _upgradeHandler;
    private LevelBalanceHandler _balanceHandler;

    public void Init(UpgradeHandler newUpgradeHandler, MoneyHandler moneyHandler, LevelBalanceHandler newBalance)
    {
        _shopPanel.Dispose();
        _levelBalancePanel.Dispose();
        _statsPanel.Dispose();

        _upgradeHandler = newUpgradeHandler;
        _balanceHandler = newBalance;
        
        moneyHandler.OnCoinsUpdate += UpdateCoinsCount;
        moneyHandler.UpdateCoins();
        UpdateLevelInfo();
        UpdateShopInfo();
        UpdateStatsInfo();
    }
    
    private void UpdateCoinsCount(int count)
    {
        _totalCoinsCount.text = count.ToString();
    }

    private void UpdateLevelInfo()
    {
        _totalRoadCount.value = PlayerPrefs.GetInt(SaveKeyTemplates.RoadCountKey);
        _totalEnemyReward.value = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyRewardKey);
        _totalEnemyCount.value = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyCountKey);
        _enemyHealth.text = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyHealthKey).ToString();
        _bossHealth.text = PlayerPrefs.GetInt(SaveKeyTemplates.BossHealthKey).ToString();
    }

    private void UpdateShopInfo()
    {
        _attackDamagePrice.text = UpgradeHandler.AttackDamageUpgradePrice.ToString();
        _attackSpeedPrice.text = UpgradeHandler.AttackSpeedUpgradePrice.ToString();
        
        _totalAttackSpeed.text = PlayerPrefs.GetFloat(SaveKeyTemplates.AttackSpeedKey).ToString(CultureInfo.InvariantCulture);
        _totalAttackDamage.text = PlayerPrefs.GetInt(SaveKeyTemplates.AttackDamageKey).ToString();
    }

    private void UpdateStatsInfo()
    {
        _levelCount.text = PlayerPrefs.GetInt(SaveKeyTemplates.TotalLevelsKey).ToString();
        _enemiesCount.text = PlayerPrefs.GetInt(SaveKeyTemplates.TotalEnemiesKey).ToString();
        _coinsCount.text = PlayerPrefs.GetInt(SaveKeyTemplates.TotalCoinsKey).ToString();
    }

    public void OnPlayButtonPressed()
    {
        _balanceHandler.ApplyNewBalanceValue((int)_totalRoadCount.value, SaveKeyTemplates.RoadCountKey);
        _balanceHandler.ApplyNewBalanceValue((int)_totalEnemyCount.value, SaveKeyTemplates.EnemyCountKey);
        _balanceHandler.ApplyNewBalanceValue((int)_totalEnemyReward.value, SaveKeyTemplates.EnemyRewardKey);
        _balanceHandler.ApplyNewBalanceValue(int.Parse(_enemyHealth.text), SaveKeyTemplates.EnemyHealthKey);
        _balanceHandler.ApplyNewBalanceValue(int.Parse(_bossHealth.text), SaveKeyTemplates.BossHealthKey);
        
        SceneManager.LoadScene((int)SceneIndexes.Level);
    }

    public void OnAttackSpeedButtonPressed()
    {
        _upgradeHandler.MakeUpgrade(SaveKeyTemplates.AttackSpeedKey);

        _totalAttackSpeed.text = PlayerPrefs.GetFloat(SaveKeyTemplates.AttackSpeedKey).ToString(CultureInfo.InvariantCulture);
    }

    public void OnAttackDamageButtonPressed()
    {
        _upgradeHandler.MakeUpgrade(SaveKeyTemplates.AttackDamageKey);

        _totalAttackDamage.text = PlayerPrefs.GetInt(SaveKeyTemplates.AttackDamageKey).ToString();
    }
}