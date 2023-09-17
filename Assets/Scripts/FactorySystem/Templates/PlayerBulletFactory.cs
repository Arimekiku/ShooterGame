using UnityEngine;

public class PlayerBulletFactory : GameFactory
{
    private const string BulletPrefabPath = "Prefabs/Player/PlayerBullet";

    private int _bulletAttackDamage;
    
    public PlayerBulletFactory(Transform newParent, int bulletAttackDamage) : base(newParent)
    {
        PlayerBullet bulletPrefab = Resources.Load<PlayerBullet>(BulletPrefabPath);
        
        DefaultInstancesPrefabs.Add(bulletPrefab);

        _bulletAttackDamage = bulletAttackDamage;
    }
    
    public void CreateInstance(Vector3 bulletPosition)
    {
        PlayerBullet newBullet = CreateInstance<PlayerBullet>();
        newBullet.Init(bulletPosition, _bulletAttackDamage);
    }
}