using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PlayerInfo
{
    public float SidewaysSpeed { get; private set; } = 5f;
    public Vector3 Velocity { get; private set; } = new(0, 0, 5f);
}