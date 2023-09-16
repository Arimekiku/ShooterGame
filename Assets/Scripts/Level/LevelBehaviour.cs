using System;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    [SerializeField] private LevelUIHandler LevelUIHandler;
    
    public event Action OnLevelEnd;

    private LevelInfo _levelInfo;
    private SaveDataHandler _saveDataInfo;
    
    private float _initialDistance;
    private float _currentDistance;

    public void Init(LevelInfo newInfo, SaveDataHandler dataHandler)
    {
        _levelInfo = newInfo;
        _saveDataInfo = dataHandler;

        foreach (EnemyBehaviour levelEnemy in _levelInfo.Enemies)
        {
            if (levelEnemy is BossBehaviour levelBoss)
            {
                levelBoss.OnDeath += UpdateTotalCoinsCount;
                levelBoss.SetHealthAndReward(_saveDataInfo.DataInfo.BossHealth, _saveDataInfo.DataInfo.EnemyReward);
            }
            else
            {
                levelEnemy.OnDeath += UpdateLevelCoinsCount;
                levelEnemy.SetHealthAndReward(_saveDataInfo.DataInfo.EnemyHealth, _saveDataInfo.DataInfo.EnemyReward);
            }
        }
        
        _initialDistance = _levelInfo.CalculateDistanceToLevelEnd();
        _currentDistance = _initialDistance;
    }
    
    private void FixedUpdate()
    {
        if (_levelInfo is not null)
        {
            _currentDistance = _levelInfo.CalculateDistanceToLevelEnd();
            
            LevelUIHandler.UpdateSliderBar(_currentDistance / _initialDistance);
        }
    }

    private void UpdateLevelCoinsCount(int amount)
    {
        _levelInfo.UpdateLevelCoins(amount);
        
        LevelUIHandler.UpdateCoinTextLabel(_levelInfo.EarnedCoins);
    }

    private void UpdateTotalCoinsCount(int amount)
    {
        UpdateLevelCoinsCount(amount);

        _saveDataInfo.DataInfo.CurrentCoinsInGame += _levelInfo.EarnedCoins;
        _saveDataInfo.DataInfo.TotalCoinsEarned += _levelInfo.EarnedCoins;
        _saveDataInfo.DataInfo.TotalEnemyKilled += _levelInfo.Enemies.Length;
        _saveDataInfo.DataInfo.TotalLevelPassed++;
        
        _saveDataInfo.SaveGame();
        
        OnLevelEnd.Invoke();
    }
}