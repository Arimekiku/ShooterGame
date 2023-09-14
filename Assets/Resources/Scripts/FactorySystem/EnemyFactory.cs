using UnityEngine;

public class EnemyFactory : GameInstanceFactory
{
    public EnemyFactory(Transform newParent, params MonoBehaviour[] newDefaultInstancesPrefabs) 
        : base(newParent, newDefaultInstancesPrefabs) { }
    
    public EnemyBehaviour CreateInstance()
    {
        return CreateInstance<EnemyBehaviour>();
    }
}
