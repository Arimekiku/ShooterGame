public class LevelInfo
{
    public readonly RoadBehaviour[] Roads;
    public readonly EnemyBehaviour[] Enemies;
    public readonly PlayerBehaviour Player;
    
    private RoadBehaviour _bossRoadInstance;

    public LevelInfo(RoadBehaviour[] newLevelRoads, EnemyBehaviour[] newLevelEnemies, PlayerBehaviour newPlayer)
    {
        Roads = newLevelRoads;
        Enemies = newLevelEnemies;

        Player = newPlayer;
        _bossRoadInstance = newLevelRoads[^1];
    }

    public float CalculateDistanceToLevelEnd()
    {
        return Player.transform.position.z - _bossRoadInstance.transform.position.z;
    }
}