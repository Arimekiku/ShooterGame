﻿public class LevelInfo
{
    public readonly RoadBehaviour[] Roads;
    public readonly EnemyBehaviour[] Enemies;
    
    public int EarnedCoins { get; private set; }
    
    private readonly PlayerBehaviour _player;
    private readonly RoadBehaviour _bossRoadInstance;
    
    public LevelInfo(RoadBehaviour[] newLevelRoads, EnemyBehaviour[] newLevelEnemies, PlayerBehaviour newPlayer)
    {
        Roads = newLevelRoads;
        Enemies = newLevelEnemies;

        _player = newPlayer;
        _bossRoadInstance = newLevelRoads[^1];
    }

    public float CalculateDistanceToLevelEnd()
    {
        return _player.transform.position.z - _bossRoadInstance.transform.position.z;
    }

    public void UpdateLevelCoins(int amount)
    {
        EarnedCoins += amount;
    }
}