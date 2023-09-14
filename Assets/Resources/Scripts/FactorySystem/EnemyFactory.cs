using UnityEngine;

public class EnemyFactory : GameInstanceFactory
{
    public EnemyFactory(EnemyBehaviour newDefaultInstanceBehaviour, Transform newParent) 
        : base(newDefaultInstanceBehaviour, newParent) { }

    public EnemyBehaviour CreateInstance()
    {
        if (_defaultInstanceBehaviour is not EnemyBehaviour)
            throw new("Type request mismatch");
        
        EnemyBehaviour newInstance = Object.Instantiate(_defaultInstanceBehaviour, _parent) as EnemyBehaviour;
        newInstance.Init();

        return newInstance;
    }
}
