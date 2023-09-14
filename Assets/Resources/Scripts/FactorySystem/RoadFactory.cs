using UnityEngine;

public class RoadFactory : GameInstanceFactory
{
    private readonly RoadBehaviour _bossInstancePrefab;
    
    private int _roadSegmentCount;
    
    public RoadFactory(RoadBehaviour newDefaultInstancePrefab, RoadBehaviour bossInstancePrefab, int newRoadCount, Transform newParent) 
        : base(newDefaultInstancePrefab, newParent)
    {
        _roadSegmentCount = newRoadCount;
        _bossInstancePrefab = bossInstancePrefab;
    }

    public RoadBehaviour CreateInstance()
    {
        if (_defaultInstanceBehaviour is not RoadBehaviour)
            throw new("Type request mismatch");

        MonoBehaviour selectedPrefab = _roadSegmentCount == 0 ? _bossInstancePrefab : _defaultInstanceBehaviour;
        RoadBehaviour newRoadInstance = Object.Instantiate(selectedPrefab, _parent) as RoadBehaviour;
        newRoadInstance.Init();
        
        _roadSegmentCount--;

        return newRoadInstance;
    }
}
