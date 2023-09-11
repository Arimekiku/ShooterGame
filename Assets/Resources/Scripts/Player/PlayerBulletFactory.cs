using UnityEngine;

[CreateAssetMenu(fileName = "BulletFactory", menuName = "SO/Bullet Factory")]
public class PlayerBulletFactory : ScriptableObject, IBulletFactory
{
    [SerializeField] private PlayerBullet _bulletPrefab;

    public PlayerBullet CreateInstance(Vector3 position)
    {
        PlayerBullet newBulletInstance = Instantiate(_bulletPrefab, position, Quaternion.identity);
        newBulletInstance.Init();

        return newBulletInstance;
    }
}