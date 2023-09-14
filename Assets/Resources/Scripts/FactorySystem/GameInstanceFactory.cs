using UnityEngine;

public class GameInstanceFactory : ProviderableObject
{
    protected readonly MonoBehaviour _defaultInstanceBehaviour;

    protected GameInstanceFactory(MonoBehaviour newDefaultInstanceBehaviour)
    {
        _defaultInstanceBehaviour = newDefaultInstanceBehaviour;
    }
}