using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private LevelUIHandler _levelUIHandler;

    public event Action OnLevelEnd;
    
    private InputBehaviour _inputBehaviour;
    private PlayerBehaviour _playerInstance;
    private RoadBehaviour _bossRoadInstance;

    private int _levelCoins;
    private float _initialDistance;
    private float _currentDistance;

    public void Init(PlayerBehaviour newPlayer, List<RoadBehaviour> newRoads,  List<EnemyBehaviour> enemies, BossBehaviour boss)
    {
        _playerInstance = newPlayer;
        _bossRoadInstance = newRoads[^1];
        
        _initialDistance = _playerInstance.transform.position.z - _bossRoadInstance.transform.position.z;
        _currentDistance = _initialDistance;
        
        foreach (EnemyBehaviour enemy in enemies)
            enemy.OnDeath += UpdateLevelCoinsCount;

        boss.OnDeath += UpdateTotalCoinsCount;
    }

    private void FixedUpdate()
    {
        _currentDistance = _playerInstance.transform.position.z - _bossRoadInstance.transform.position.z;
        
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