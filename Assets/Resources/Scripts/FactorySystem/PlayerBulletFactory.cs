using UnityEngine;

public class PlayerBulletFactory : GameInstanceFactory
{
    public PlayerBulletFactory(PlayerBullet newDefaultInstancePrefab, Transform newParent) 
        : base(newDefaultInstancePrefab, newParent) { }

    public PlayerBullet CreateInstance()
    {
        if (_defaultInstanceBehaviour is not PlayerBullet)
            throw new("Type request mismatch");
        
        PlayerBullet newBulletInstance = Object.Instantiate(_defaultInstanceBehaviour, _parent) as PlayerBullet;
        newBulletInstance.Init();

        return newBulletInstance;
    }
}