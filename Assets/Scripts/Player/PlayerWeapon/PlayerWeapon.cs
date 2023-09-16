using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform AttackPoint;
    
    private PlayerBulletFactory _bulletBulletFactory;
    private PlayerWeaponInfo _weaponInfo;
    private Coroutine _attackRoutine;
    
    public void Init(PlayerBulletFactory bulletBulletFactory, PlayerWeaponInfo weaponInfo)
    {
        _weaponInfo = weaponInfo;
        _bulletBulletFactory = bulletBulletFactory;
        
        _attackRoutine = StartCoroutine(Attack());
    }

    public void StopAttack()
    {
        StopCoroutine(_attackRoutine);
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            PlayerBullet newBulletInstance = _bulletBulletFactory.CreateInstance();
            newBulletInstance.SetInitialPosition(AttackPoint.position);
            newBulletInstance.SetDamage(_weaponInfo.AttackDamage);

            float timeUntilNextAttack = 1 / _weaponInfo.AttackSpeed;
            yield return new WaitForSeconds(timeUntilNextAttack);
        }
    }
}