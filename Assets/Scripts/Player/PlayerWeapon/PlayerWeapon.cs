using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform AttackPoint;
    
    private float _attackSpeed;
    private PlayerBulletFactory _bulletBulletFactory;
    private Coroutine _attackRoutine;
    
    public void Init(PlayerBulletFactory bulletBulletFactory, float attackSpeed)
    {
        _bulletBulletFactory = bulletBulletFactory;
        _attackSpeed = attackSpeed;
    }

    public void StopAttack()
    {
        StopCoroutine(_attackRoutine);
    }
    
    public void StartAttack()
    {
        _attackRoutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            _bulletBulletFactory.CreateInstance(AttackPoint.position);

            float timeUntilNextAttack = 1 / _attackSpeed;
            yield return new WaitForSeconds(timeUntilNextAttack);
        }
    }
}