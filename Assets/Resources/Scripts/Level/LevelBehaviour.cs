using System;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    [SerializeField] private LevelUIHandler _levelUIHandler;
    
    public event Action OnLevelEnd;

    private LevelInfo _levelInfo;
    
    private int _levelCoins;
    private float _initialDistance;
    private float _currentDistance;

    public void Init(LevelInfo newInfo)
    {
        _levelInfo = newInfo;

        foreach (EnemyBehaviour levelEnemy in _levelInfo.Enemies)
        {
            if (levelEnemy is BossBehaviour levelBoss)
                levelBoss.OnDeath += UpdateTotalCoinsCount;
            else
                levelEnemy.OnDeath += UpdateLevelCoinsCount;
        }
        
        _initialDistance = _levelInfo.CalculateDistanceToLevelEnd();
        _currentDistance = _initialDistance;
    }
    
    private void FixedUpdate()
    {
        _currentDistance = _levelInfo.CalculateDistanceToLevelEnd();
        
        _levelUIHandler.UpdateSlider(_currentDistance / _initialDistance);
    }

    private void UpdateLevelCoinsCount(int amount)
    {
        _levelCoins += amount;
        _levelUIHandler.UpdateCoins(_levelCoins);
    }

    private void UpdateTotalCoinsCount(int amount)
    {
        UpdateLevelCoinsCount(amount);
        
        int cachedCoins = PlayerPrefs.GetInt(SaveKeyTemplates.CurrentCoinsKey);
        
        PlayerPrefs.SetInt(SaveKeyTemplates.CurrentCoinsKey, cachedCoins + _levelCoins);
        
        TotalStatsHandler.UpdateKey(SaveKeyTemplates.TotalCoinsKey, _levelCoins);
        TotalStatsHandler.UpdateKey(SaveKeyTemplates.TotalLevelsKey, 1);
        
        OnLevelEnd.Invoke();
    }
}