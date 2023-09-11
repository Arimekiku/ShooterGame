using UnityEngine;

[CreateAssetMenu(fileName = "RoadFactory", menuName = "SO/Road Factory")]
public class RoadFactory : ScriptableObject, IRoadFactory
{
    [SerializeField] private RoadBehaviour _roadPrefab;
    [SerializeField] private RoadBehaviour _bossRoadPrefab;
    
    public float TotalTrackLength { get; private set; }
    
    private Transform _parent;
    private Vector3 _segmentSpawnPosition;
    private int _roadSegmentCount;

    public void Init(int newRoadCount, Transform newParent)
    {
        _roadSegmentCount = newRoadCount;

        TotalTrackLength = 0;
        _segmentSpawnPosition = Vector3.zero;

        _parent = newParent;
    }

    public RoadBehaviour CreateInstance()
    {
        RoadBehaviour newRoadInstance = Instantiate(_roadSegmentCount == 0 ? _bossRoadPrefab : _roadPrefab, _parent);
        newRoadInstance.transform.position = _segmentSpawnPosition;
        newRoadInstance.Init();
        
        Vector3 offset = new(0, 0, newRoadInstance.RoadLength);
        _segmentSpawnPosition += offset;
        TotalTrackLength += newRoadInstance.RoadLength;

        _roadSegmentCount--;

        return newRoadInstance;
    }
}
