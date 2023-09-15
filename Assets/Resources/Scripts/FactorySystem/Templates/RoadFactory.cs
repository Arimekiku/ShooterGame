using UnityEngine;

public class RoadFactory : GameInstanceFactory
{
    public RoadFactory(Transform newContainer, params MonoBehaviour[] newDefaultInstancesPrefabs) 
        : base(newContainer, newDefaultInstancesPrefabs) { }

    public RoadBehaviour CreateDefaultInstance()
    {
        return CreateInstance<RoadBehaviour>();
    }

    public BossRoadBehaviour CreateBossInstance()
    {
        return CreateInstance<BossRoadBehaviour>();
    }
}
