using UnityEngine;

public class EnemyFactory : GameInstanceFactory
{
    private readonly Transform _parent;
    private readonly Vector3 _offset;
    private Vector3 _enemySpawnPosition;
    
    public EnemyFactory(EnemyBehaviour newDefaultInstanceBehaviour, float rangeBetweenEnemies, Transform newParent) 
        : base(newDefaultInstanceBehaviour)
    {
        _offset = new(0, 0, rangeBetweenEnemies);

        _enemySpawnPosition = Vector3.zero;
        _parent = newParent;
    }

    public EnemyBehaviour CreateInstance()
    {
        if (_defaultInstanceBehaviour is not EnemyBehaviour)
            throw new("Type request mismatch");
        
        EnemyBehaviour newInstance = Object.Instantiate(_defaultInstanceBehaviour, _parent) as EnemyBehaviour;
        newInstance.transform.position = _enemySpawnPosition;
        
        newInstance.Init();

        _enemySpawnPosition = new Vector3(0, 0, _enemySpawnPosition.z) + _offset;
        _enemySpawnPosition.x = Random.Range(-2f, 2f);

        return newInstance;
    }
}
