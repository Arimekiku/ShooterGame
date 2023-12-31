﻿using System;
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
        
        _initialDistance = _levelInfo.DistanceToLevelEnd;
    }
    
    private void FixedUpdate()
    {
        if (_levelInfo is not null)
        {
            _currentDistance = _levelInfo.DistanceToLevelEnd;
            
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
        
        OnLevelEnd.Invoke();
    }
}