using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IData
{
    protected readonly List<MonoBehaviour> DefaultInstancesPrefabs;
    private readonly Transform _container;

    protected GameFactory(Transform newContainer)
    {
        DefaultInstancesPrefabs = new();
        _container = newContainer;
    }
    
    protected T CreateInstance<T>() where T : BuildableObject
    {
        if (DefaultInstancesPrefabs.Find(p => p is T) is not T typePrefab)
            throw new("Type request not found");

        T newInstance = Object.Instantiate(typePrefab, _container);
        newInstance.Init();
        
        return newInstance;
    }
}