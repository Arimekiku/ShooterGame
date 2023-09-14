using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    
    private float _attackSpeed;
    private PlayerBulletFactory _bulletBulletFactory;
    private Coroutine _attackRoutine;
    
    public void Init(PlayerBulletFactory bulletBulletFactory)
    {
        _attackSpeed = PlayerPrefs.GetFloat(SaveKeyTemplates.AttackSpeedKey);
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
            _bulletBulletFactory.CreateInstance(_attackPoint.position);

            float timeUntilNextAttack = 1 / _attackSpeed;
            yield return new WaitForSeconds(timeUntilNextAttack);
        }
    }
}