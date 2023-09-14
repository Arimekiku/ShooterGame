using UnityEngine;

public class PlayerBulletFactory : GameInstanceFactory
{
    public PlayerBulletFactory(Transform newParent, params MonoBehaviour[] newDefaultInstancesPrefabs)
        : base(newParent, newDefaultInstancesPrefabs) { }
    
    public PlayerBullet CreateInstance()
    {
        return CreateInstance<PlayerBullet>();
    }
}