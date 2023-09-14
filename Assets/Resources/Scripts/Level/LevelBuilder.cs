using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelBuilder
{
    private InputBehaviour _inputBehaviour;
    private readonly PlayerBehaviour _playerInstance;

    private int _levelCoins;
    private float _initialDistance;
    private float _currentDistance;

    private readonly RoadBehaviour[] _roads;
    private readonly EnemyBehaviour[] _enemies;

    public LevelBuilder(PlayerBehaviour newPlayer, List<RoadBehaviour> newRoads,  List<EnemyBehaviour> newEnemies)
    {
        _playerInstance = newPlayer;

        _roads = newRoads.ToArray();
        _enemies = newEnemies.ToArray();
        
        float totalTrackLength = ArrangeRoadSegments(newRoads);
        ArrangeEnemies(newEnemies, totalTrackLength / newEnemies.Count);
    }

    private void ArrangeEnemies(List<EnemyBehaviour> enemies, float rangeBetweenEnemies)
    {
        Vector3 enemySpawnPosition = Vector3.zero;
        Vector3 offset = new(0, 0, rangeBetweenEnemies);
        
        foreach (EnemyBehaviour enemy in enemies)
        {
            enemy.transform.position = enemySpawnPosition;
            enemySpawnPosition = new Vector3(0, 0, enemySpawnPosition.z) + offset;
            enemySpawnPosition.x = Random.Range(-2f, 2f);
        }
    }

    private float ArrangeRoadSegments(List<RoadBehaviour> newRoads)
    {
        float totalTrackLength = 0;
        Vector3 segmentSpawnPosition = Vector3.zero;
        
        foreach (RoadBehaviour roadBehaviour in newRoads)
        {
            roadBehaviour.transform.position = segmentSpawnPosition;
            roadBehaviour.Init();

            Vector3 offset = new(0, 0, roadBehaviour.RoadLength);
            segmentSpawnPosition += offset;
            totalTrackLength += roadBehaviour.RoadLength;
        }

        return totalTrackLength;
    }

    public LevelInfo BuildLevelInfo()
    {
        return new(_roads, _enemies, _playerInstance);
    }
}