using UnityEngine;
using UnityEngine.Serialization;

public class RoadBehaviour : MonoBehaviour
{
    [SerializeField] private Transform GroundTransform;
     
    public float RoadLength { get; private set; }
    
    public void Init()
    {
        RoadLength = GroundTransform.localScale.z;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour player))
            player.Death();
    }
}