using UnityEngine;

public class RoadBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _groundTransform;
     
    public float RoadLength { get; private set; }
    
    public virtual void Init()
    {
        RoadLength = _groundTransform.localScale.z;
    }
}