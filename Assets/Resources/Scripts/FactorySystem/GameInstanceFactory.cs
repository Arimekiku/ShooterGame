using UnityEngine;

public class GameInstanceFactory : ProviderableObject
{
    protected readonly MonoBehaviour _defaultInstanceBehaviour;
    protected readonly Transform _parent;

    protected GameInstanceFactory(MonoBehaviour newDefaultInstanceBehaviour, Transform newParent)
    {
        _defaultInstanceBehaviour = newDefaultInstanceBehaviour;
        _parent = newParent;
    }
}