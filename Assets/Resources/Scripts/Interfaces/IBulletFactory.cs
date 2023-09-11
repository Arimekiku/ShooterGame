using UnityEngine;

public interface IBulletFactory
{
    public PlayerBullet CreateInstance(Vector3 position);
}