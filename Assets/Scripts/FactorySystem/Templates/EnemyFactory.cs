using UnityEngine;

public class EnemyFactory : GameFactory
{
    private const string EnemyPrefabPath = "Prefabs/Enemies/DefaultEnemy/Enemy";
    
    public EnemyFactory(Transform newParent) : base(newParent)
    {
        EnemyBehaviour enemyPrefab = Resources.Load<EnemyBehaviour>(EnemyPrefabPath);
        
        DefaultInstancesPrefabs.Add(enemyPrefab);
    }
    
    public EnemyBehaviour CreateInstance()
    {
        return CreateInstance<EnemyBehaviour>();
    }
}
