using UnityEngine;
using UnityEngine.Serialization;

public class RoadBehaviour : BuildableObject
{
    [FormerlySerializedAs("_groundTransform")] [SerializeField] private Transform GroundTransform;
     
    public float RoadLength { get; private set; }
    
    public override void Init()
    {
        RoadLength = GroundTransform.localScale.z;
    }
}