using UnityEngine;

public class RoadBehaviour : BuildableObject
{
    [SerializeField] private Transform _groundTransform;
     
    public float RoadLength { get; private set; }
    
    public override void Init()
    {
        RoadLength = _groundTransform.localScale.z;
    }
}