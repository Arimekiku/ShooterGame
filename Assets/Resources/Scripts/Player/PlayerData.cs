using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _sidewaysSpeed;
    
    public float SidewaysSpeed => _sidewaysSpeed;
    public Vector3 Velocity => _velocity;
}