using System.Collections.Generic;
using UnityEngine;

public class GameInstanceFactory : ProviderableObject
{
    private readonly List<MonoBehaviour> _defaultInstancesPrefabs;
    private readonly Transform _parent;

    protected GameInstanceFactory(Transform newParent, params MonoBehaviour[] newDefaultInstancesPrefabs)
    {
        _defaultInstancesPrefabs = new(newDefaultInstancesPrefabs);
        _parent = newParent;
    }
    
    protected T CreateInstance<T>() where T : BuildableObject
    {
        if (_defaultInstancesPrefabs.Find(p => p is T) is not T typePrefab)
            throw new("Type request not found");

        T newInstance = Object.Instantiate(typePrefab, _parent);
        newInstance.Init();
        
        return newInstance;
    }
}