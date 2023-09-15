using UnityEngine;

public class RoadFactory : GameFactory
{
    private const string DefaultRoadPrefabPath = "Prefabs/Track/RoadSegment";
    private const string BossRoadPrefabPath = "Prefabs/Track/BossRoadSegment";
    
    public RoadFactory(Transform newContainer) : base(newContainer)
    {
        RoadBehaviour defaultRoadPrefab = Resources.Load<RoadBehaviour>(DefaultRoadPrefabPath);
        BossRoadBehaviour bossRoadPrefab = Resources.Load<BossRoadBehaviour>(BossRoadPrefabPath);
        
        DefaultInstancesPrefabs.Add(defaultRoadPrefab);
        DefaultInstancesPrefabs.Add(bossRoadPrefab);
    }

    public RoadBehaviour CreateDefaultInstance()
    {
        return CreateInstance<RoadBehaviour>();
    }

    public BossRoadBehaviour CreateBossInstance()
    {
        return CreateInstance<BossRoadBehaviour>();
    }
}
