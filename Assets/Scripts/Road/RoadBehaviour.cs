using UnityEngine;
using UnityEngine.Serialization;

public class RoadBehaviour : MonoBehaviour
{
    [SerializeField] private Transform GroundTransform;
     
    public float RoadLength { get; private set; }
    
    public virtual void Init()
    {
        RoadLength = GroundTransform.localScale.z;
    }
}