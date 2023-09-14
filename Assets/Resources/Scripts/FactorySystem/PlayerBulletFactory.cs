using UnityEngine;

public class PlayerBulletFactory : GameInstanceFactory
{
    public PlayerBulletFactory(PlayerBullet newDefaultInstancePrefab) 
        : base(newDefaultInstancePrefab) { }

    public PlayerBullet CreateInstance(Vector3 position)
    {
        if (_defaultInstanceBehaviour is not PlayerBullet)
            throw new("Type request mismatch");
        
        PlayerBullet newBulletInstance = Object.Instantiate(_defaultInstanceBehaviour, position, Quaternion.identity) as PlayerBullet;
        newBulletInstance.Init();

        return newBulletInstance;
    }
}