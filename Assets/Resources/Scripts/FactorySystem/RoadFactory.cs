using UnityEngine;

public class RoadFactory : GameInstanceFactory
{
    private readonly RoadBehaviour _bossInstancePrefab;
    
    public RoadFactory(Transform newParent, params MonoBehaviour[] newDefaultInstancesPrefabs) 
        : base(newParent, newDefaultInstancesPrefabs) { }

    public RoadBehaviour CreateDefaultInstance()
    {
        return CreateInstance<RoadBehaviour>();
    }

    public BossRoadBehaviour CreateBossInstance()
    {
        return CreateInstance<BossRoadBehaviour>();
    }
}
