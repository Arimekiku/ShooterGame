using UnityEngine;

public class RoadFactory : GameInstanceFactory
{
    public float TotalTrackLength { get; private set; }
    
    private readonly RoadBehaviour _bossInstancePrefab;
    private readonly Transform _parent;
    
    private Vector3 _segmentSpawnPosition;
    private int _roadSegmentCount;
    
    public RoadFactory(RoadBehaviour newDefaultInstancePrefab, RoadBehaviour bossInstancePrefab, int newRoadCount, Transform newParent) 
        : base(newDefaultInstancePrefab)
    {
        _roadSegmentCount = newRoadCount;

        TotalTrackLength = 0;
        _segmentSpawnPosition = Vector3.zero;

        _parent = newParent;
        _bossInstancePrefab = bossInstancePrefab;
    }

    public RoadBehaviour CreateInstance()
    {
        if (_defaultInstanceBehaviour is not RoadBehaviour)
            throw new("Type request mismatch");

        MonoBehaviour selectedPrefab = _roadSegmentCount == 0 ? _bossInstancePrefab : _defaultInstanceBehaviour;
        
        RoadBehaviour newRoadInstance = Object.Instantiate(selectedPrefab, _parent) as RoadBehaviour;
        newRoadInstance.transform.position = _segmentSpawnPosition;
        newRoadInstance.Init();
        
        Vector3 offset = new(0, 0, newRoadInstance.RoadLength);
        _segmentSpawnPosition += offset;
        TotalTrackLength += newRoadInstance.RoadLength;

        _roadSegmentCount--;

        return newRoadInstance;
    }
}
