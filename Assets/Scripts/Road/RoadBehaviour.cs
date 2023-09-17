using UnityEngine;
using UnityEngine.Serialization;

public class RoadBehaviour : BuildableObject
{
    [SerializeField] private Transform GroundTransform;
     
    public float RoadLength { get; private set; }
    
    public override void Init()
    {
        RoadLength = GroundTransform.localScale.z;
    }
}