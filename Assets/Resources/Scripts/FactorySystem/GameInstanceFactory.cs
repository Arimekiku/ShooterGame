using System.Collections.Generic;
using UnityEngine;

public class GameInstanceFactory : IData
{
    private readonly List<MonoBehaviour> _defaultInstancesPrefabs;
    private readonly Transform _container;

    protected GameInstanceFactory(Transform newContainer, params MonoBehaviour[] newDefaultInstancesPrefabs)
    {
        _defaultInstancesPrefabs = new(newDefaultInstancesPrefabs);
        _container = newContainer;
    }
    
    protected T CreateInstance<T>() where T : BuildableObject
    {
        if (_defaultInstancesPrefabs.Find(p => p is T) is not T typePrefab)
            throw new("Type request not found");

        T newInstance = Object.Instantiate(typePrefab, _container);
        newInstance.Init();
        
        return newInstance;
    }
}