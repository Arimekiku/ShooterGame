﻿using UnityEngine;
using Random = UnityEngine.Random;

public class LevelBuilder
{
    private readonly PlayerBehaviour _playerInstance;
    private readonly int _enemyCount;
    private readonly int _roadSegmentCount;
    
    private RoadBehaviour[] _roads;
    private EnemyBehaviour[] _enemies;

    public LevelBuilder(PlayerBehaviour newPlayer, DataProvider<GameFactory> newFactoryProvider, int roadCount, int enemyCount)
    {
        _playerInstance = newPlayer;

        _roadSegmentCount = roadCount;
        _enemyCount = enemyCount;
        
        BuildRoads(newFactoryProvider);
        float totalTrackLength = ArrangeRoadSegments();
        
        BuildEnemies(newFactoryProvider);
        ArrangeEnemies(totalTrackLength / _enemies.Length - 1);
    }
    
    public LevelInfo BuildLevelInfo()
    {
        return new(_roads, _enemies, _playerInstance);
    }
    
    private void BuildRoads(DataProvider<GameFactory> factoryProvider)
    {
        RoadFactory roadFactory = factoryProvider.GetObjectOfType<RoadFactory>();
        
        _roads = new RoadBehaviour[_roadSegmentCount];
        _enemies = new EnemyBehaviour[_enemyCount];
        
        for (int i = 0; i < _roadSegmentCount - 1; i++)
        {
            RoadBehaviour roadInstance = roadFactory.CreateDefaultInstance();
            _roads[i] = roadInstance;
        }
        
        BossRoadBehaviour bossRoadInstance = roadFactory.CreateBossInstance();
        _roads[_roadSegmentCount - 1] = bossRoadInstance;
        _enemies[^1] = bossRoadInstance.Boss;
    }
    
    private float ArrangeRoadSegments()
    {
        float totalTrackLength = 0;
        Vector3 segmentSpawnPosition = Vector3.zero;

        foreach (RoadBehaviour road in _roads)
        {
            road.transform.position = segmentSpawnPosition;
            road.Init();
            
            if (road is BossRoadBehaviour)
                continue;

            Vector3 offset = new(0, 0, road.RoadLength);
            segmentSpawnPosition += offset;
            totalTrackLength += road.RoadLength;
        }

        return totalTrackLength;
    }
    
    private void BuildEnemies(DataProvider<GameFactory> factoryProvider)
    {
        EnemyFactory enemyFactory = factoryProvider.GetObjectOfType<EnemyFactory>();

        for (int i = 0; i < _enemyCount - 1; i++)
        {
            EnemyBehaviour enemyInstance = enemyFactory.CreateInstance();
            _enemies[i] = enemyInstance;
        }
    }

    private void ArrangeEnemies(float rangeBetweenEnemies)
    {
        Vector3 enemySpawnPosition = Vector3.zero;
        Vector3 offset = new(0, 0, rangeBetweenEnemies);
        
        for (int i = 0; i < _enemyCount - 1; i++)
        {
            _enemies[i].transform.position = enemySpawnPosition;
            enemySpawnPosition = new Vector3(0, 0, enemySpawnPosition.z) + offset;
            enemySpawnPosition.x = Random.Range(-2f, 2f);
        }
    }
}